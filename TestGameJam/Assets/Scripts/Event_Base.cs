using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_Base : MonoBehaviour {

    public float m_eventDuration = 5.0f;        // Length the event is in existence for

   public void EventLifetime()
   {
        Destroy(this.gameObject, m_eventDuration);
   }
}
