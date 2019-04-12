using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_Press : Event_Base {

    public float m_pressDamage;

	// Use this for initialization
	void Start () {
        EventLifetime();
	}

    private void OnTriggerStay(Collider other)
    {
        other.GetComponent<PlayerController>().m_fDamagePercent = other.GetComponent<PlayerController>().m_fDamagePercent + m_pressDamage;
    }
}
