using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_Fire : Event_Base {

    private float m_timer = 0;
    public float m_timeBetweenDamage;
    public float m_burnDamage = 3.0f;

	// Use this for initialization
	void Start () {
        EventLifetime();
	}

    private void OnTriggerStay(Collider other)
    {
        if(m_timer >= m_timeBetweenDamage)
        {
            other.GetComponent<PlayerController>().m_fDamagePercent = other.GetComponent<PlayerController>().m_fDamagePercent + m_burnDamage;
            m_timer = 0;
        }
        m_timer += Time.deltaTime;
    }
}
