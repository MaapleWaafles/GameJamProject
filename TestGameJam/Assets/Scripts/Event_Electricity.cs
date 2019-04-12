using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_Electricity : Event_Base {

    public float m_shockDamage;    

	// Use this for initialization
	void Start () {
        EventLifetime();
	}

    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<PlayerController>().m_fDamagePercent = other.GetComponent<PlayerController>().m_fDamagePercent + m_shockDamage;
    }

    private void OnTriggerStay(Collider other)
    {
        other.GetComponent<PlayerController>().m_movementSpeed = 0;
    }

    private void OnTriggerExit(Collider other)
    {
        other.GetComponent<PlayerController>().m_movementSpeed = 10f;
    }
}
