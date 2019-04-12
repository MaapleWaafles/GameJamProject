using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinGameScreen : MonoBehaviour {

    public PlayerController[] players;
    public Canvas WinGame;

    // Use this for initialization
    void Start() {
        players = FindObjectsOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update() {
        if (players[3].m_playerNumber == 4) {
            if (players[0].m_health > 0 && players[1].m_health == 0 && players[2].m_health == 0 && players[3].m_health == 0) {
                players[1].isAlive = false;
                players[2].isAlive = false;
                players[3].isAlive = false;
                if (players[0].isAlive == true && players[0].m_health > 0) {
                    WinTheGame();
                }
            }
            if (players[1].m_health > 0 && players[0].m_health == 0 && players[2].m_health == 0 && players[3].m_health == 0)
            {
                players[0].isAlive = false;
                players[2].isAlive = false;
                players[3].isAlive = false;
                if (players[1].isAlive == true && players[1].m_health > 0)
                {
                    WinTheGame();
                }
            }
            if (players[2].m_health > 0 && players[0].m_health == 0 && players[1].m_health == 0 && players[3].m_health == 0)
            {
                players[0].isAlive = false;
                players[1].isAlive = false;
                players[3].isAlive = false;
                if (players[2].isAlive == true && players[2].m_health > 0)
                {
                    WinTheGame();
                }
            }
            if (players[3].m_health > 0 && players[0].m_health == 0 && players[1].m_health == 0 && players[2].m_health == 0)
            {
                players[0].isAlive = false;
                players[1].isAlive = false;
                players[2].isAlive = false;
                if (players[3].isAlive == true && players[3].m_health > 0)
                {
                    WinTheGame();
                }
            }

        }
        if (players[2].m_playerNumber == 3)
        {
            if (players[0].m_health > 0 && players[1].m_health == 0 && players[2].m_health == 0)
            {
                players[1].isAlive = false;
                players[2].isAlive = false;
                if (players[0].isAlive == true && players[0].m_health > 0)
                {
                    WinTheGame();
                }
            }
            if (players[1].m_health > 0 && players[0].m_health == 0 && players[2].m_health == 0)
            {
                players[0].isAlive = false;
                players[2].isAlive = false;
                if (players[1].isAlive == true && players[1].m_health > 0)
                {
                    WinTheGame();
                }
            }
            if (players[2].m_health > 0 && players[0].m_health == 0 && players[1].m_health == 0)
            {
                players[0].isAlive = false;
                players[1].isAlive = false;
                if (players[2].isAlive == true && players[2].m_health > 0)
                {
                    WinTheGame();
                }
            }
        }
        if (players[1].m_playerNumber == 2)
        {
            if (players[0].m_health > 0 && players[1].m_health == 0)
            {
                players[1].isAlive = false;
                if (players[0].isAlive == true && players[0].m_health > 0)
                {
                    WinTheGame();
                }
            }
            if (players[1].m_health > 0 && players[0].m_health == 0)
            {
                players[0].isAlive = false;
                if (players[1].isAlive == true && players[1].m_health > 0)
                {
                    WinTheGame();
                }
            }
        }
    }

    void WinTheGame() {
        WinGame.enabled = true;
    }
}
