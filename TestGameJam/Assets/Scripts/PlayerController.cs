using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using XInputDotNetPure;

public class PlayerController : MonoBehaviour
{
    public int m_playerNumber = 1;
    public float m_movementSpeed = 10f;
    public float m_rotateSpeed = 0.5f;
    public float m_jumpVelocity = 20f;
    public float m_thumbstickDeadzone = 0.15f;
    public float m_punchCooldownTime = 0.2f;
    public float m_punchDistance = 2f;
    public float m_punchSpeedOverTimeMultiplier = 2f;
    public float m_slowdownWhenPunch = 0.2f;
    public float m_baseKnockbackForce = 500f;
    public float m_knockbackForce = 3000f;
    public float m_forceToDamageFactor = 0.01f;
    public float m_fHittingVibrationStrength = 0.5f;
    public float m_fHittingVibrationTime = 0.1f;
    public float m_fHitVibrationStrength = 1f;
    public float m_fHitVibrationTime = 0.5f;
    public List<GameObject> m_fists;

    [HideInInspector]
    public bool m_bPunching = false;
    [HideInInspector]
    public float m_fDamagePercent = 0f;

    private Rigidbody m_rigidbody;
    private Vector3[] m_fistStartPos;
    private float m_fCurrentPunchCooldown = 0f;
    private float m_fCurrentPunchSpeed = 0f;
    private float m_fVibrationTimer = 0f;
    // 0 = left, 1 = right
    private int m_iFist = 0;
    [HideInInspector]
    public uint m_health = 3;
    [HideInInspector]
    public bool isAlive = true;

    // Use this for initialization
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        m_fistStartPos = new Vector3[2];
        for (int i = 0; i < 2; i++)
        {
            // ignore collision to fists
            Physics.IgnoreCollision(GetComponent<Collider>(), m_fists[i].GetComponent<Collider>(), true);
            m_fistStartPos[i] = m_fists[i].transform.localPosition;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // get controller input
        GamePadState gamePadState = GamePad.GetState((PlayerIndex)m_playerNumber - 1);

        // movement input
        Vector3 v3Velocity = m_rigidbody.velocity;
        if (Mathf.Abs(gamePadState.ThumbSticks.Left.X) > m_thumbstickDeadzone || Mathf.Abs(gamePadState.ThumbSticks.Left.Y) > m_thumbstickDeadzone)
        {
            v3Velocity.x += gamePadState.ThumbSticks.Left.X * m_movementSpeed * Time.deltaTime;
            v3Velocity.z += gamePadState.ThumbSticks.Left.Y * m_movementSpeed * Time.deltaTime;
        }
        // jump
        if (gamePadState.Buttons.A == ButtonState.Pressed
            || gamePadState.Triggers.Left > 0f)
            if (Physics.Raycast(transform.position, Vector3.down, 1.5f))
                v3Velocity.y = m_jumpVelocity;
        // set new velocity
        m_rigidbody.velocity = v3Velocity;

        // look towards right stick input
        if (Mathf.Abs(gamePadState.ThumbSticks.Right.X) > m_thumbstickDeadzone || Mathf.Abs(gamePadState.ThumbSticks.Right.Y) > m_thumbstickDeadzone)
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(new Vector3(gamePadState.ThumbSticks.Right.X, 0, gamePadState.ThumbSticks.Right.Y)), m_rotateSpeed);

        // start punch
        if (gamePadState.Triggers.Right > 0f
            && m_fCurrentPunchCooldown <= 0f
            && !m_bPunching)
        {
            // set punch to true
            m_bPunching = true;
            // slow velocity when punch
            Vector3 v3NewVelocity = m_rigidbody.velocity;
            v3NewVelocity.x *= m_slowdownWhenPunch;
            v3NewVelocity.z *= m_slowdownWhenPunch;
            m_rigidbody.velocity = v3NewVelocity;
        }

        // punch but stop punch if function returns false
        if (m_bPunching
            && !Punch(m_iFist))
        {
            m_bPunching = false; // stop punching
            m_fCurrentPunchCooldown = m_punchCooldownTime; // set cooldown
            m_fCurrentPunchSpeed = 0;
            // change fist index
            switch (m_iFist)
            {
                case 0:
                    m_iFist = 1;
                    break;
                case 1:
                    m_iFist = 0;
                    break;
            }
        }

        // decrement cooldown of punch if not punching
        if (!m_bPunching
            && m_fCurrentPunchCooldown > 0f)
        {
            m_fCurrentPunchCooldown -= Time.deltaTime;
        }

        m_fVibrationTimer -= Time.deltaTime;
        if (m_fVibrationTimer <= 0f)
        {
            GamePad.SetVibration((PlayerIndex)m_playerNumber - 1, 0f, 0f);
        }
    }

    // returns false when punch has ended
    // otherwise returns true
    private bool Punch(int iFist)
    {
        // move fist
        m_fists[iFist].GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(1, Mathf.Lerp(0, 100, m_fCurrentPunchSpeed));
        m_fCurrentPunchSpeed += Time.deltaTime * m_punchSpeedOverTimeMultiplier;

        // i cannot stress how terrible this workaround is to match the collider to the boxing glove, but im running out of time!!
        float fTravelled = m_fists[iFist].GetComponent<SkinnedMeshRenderer>().GetBlendShapeWeight(1) / 100;
        m_fists[iFist].GetComponent<SphereCollider>().center = new Vector3(13.63f + (20f * fTravelled), 6.2f, 46.35f + (213.65f * fTravelled));
        m_fists[iFist].GetComponent<SphereCollider>().radius = 34.6f + (15 * fTravelled);

        if (m_fists[iFist].GetComponent<SkinnedMeshRenderer>().GetBlendShapeWeight(1) >= 100f)
        {
            m_fists[iFist].GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(1, 0);
            m_fists[iFist].GetComponent<SphereCollider>().center = new Vector3(13.63f, 6.2f, 46.35f);
            m_fists[iFist].GetComponent<SphereCollider>().radius = 34.6f;
            return false;
        }
        return true;
    }

    public void Vibrate(float fVibrateStrength, float fVibrateTime)
    {
        GamePad.SetVibration((PlayerIndex)m_playerNumber - 1, fVibrateStrength, fVibrateStrength);
        m_fVibrationTimer = fVibrateTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // if hit by punching enemy fist
        if (collision.gameObject.CompareTag("Fist")
            && collision.gameObject.GetComponentInParent<PlayerController>().m_bPunching)
        {
            // calculate force
            float fForce = m_baseKnockbackForce + (m_knockbackForce * (m_fDamagePercent / 100f));
            // apply force
            m_rigidbody.AddExplosionForce(fForce, collision.transform.position, 10f);
            m_rigidbody.AddExplosionForce(fForce, collision.contacts[0].point, 10f);
            collision.gameObject.GetComponentInParent<PlayerController>().Vibrate(m_fHittingVibrationStrength, m_fHittingVibrationTime);
            Vibrate(m_fHitVibrationStrength, m_fHitVibrationTime);
            m_fDamagePercent += fForce * m_forceToDamageFactor; // add damage
        }
    }
}