using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameObject[] m_players;
	// Use this for initialization
	void Awake()
    {
        GameObject playerCount = GameObject.FindGameObjectWithTag("PlayerCount");
        for (int i = 0; i < 4; i++)
        {
            // if player isn't playing
            if (playerCount
                && !playerCount.GetComponent<PlayerCountSingleton>().m_bPlayersActive[i])
            {
                // deactive their object
                m_players[i].SetActive(false);
            }
        }
	}
}