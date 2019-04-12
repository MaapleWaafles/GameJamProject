using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using XInputDotNetPure;

public class PlayerCountSingleton : MonoBehaviour
{
    public GameObject m_playerJoinScreen;
    public GameObject m_mainMenuScreen;
    public GameObject[] m_playerJoinedText;
    public GameObject m_pressStart;

    [HideInInspector]
    public bool[] m_bPlayersActive;

    private int m_nPlayerCount = 0;

	// Use this for initialization
	void Start()
    {
        DontDestroyOnLoad(gameObject);
        m_bPlayersActive = new bool[4];
	}
	
	// Update is called once per frame
	void Update()
    {
        // dont run if not on player join screen
        if (m_playerJoinScreen
            && !m_playerJoinScreen.activeSelf)
            return;

        if (!m_pressStart)
            return;

        for (int i = 0; i < 4; ++i)
        {
            // get player input
            GamePadState gamePadState = GamePad.GetState((PlayerIndex)i);

            // check if player pressed a and if they havent already
            if (gamePadState.Buttons.A == ButtonState.Pressed
                && !m_bPlayersActive[i])
            {
                m_playerJoinedText[i].SetActive(true);
                m_bPlayersActive[i] = true;
                m_nPlayerCount++;
            }

            if (m_nPlayerCount > 1
                && !m_pressStart.activeSelf)
            {
                m_pressStart.SetActive(true);
            }
            else if (m_nPlayerCount < 2
                && m_pressStart.activeSelf)
            {
                m_pressStart.SetActive(false);
            }
            // if the player pressed start and there is more than one player
            if (gamePadState.Buttons.Start == ButtonState.Pressed
                && m_nPlayerCount > 1)
            {
                // start game
                SceneManager.LoadScene(1);
            }

            if (gamePadState.Buttons.B == ButtonState.Pressed)
            {
                // set this player to not active if they are active
                if (m_bPlayersActive[i])
                {
                    m_playerJoinedText[i].SetActive(false);
                    m_bPlayersActive[i] = false;
                    m_nPlayerCount--;
                }
                // go back to main menu if player is not active
                else
                {
                    // remove all players from active
                    for (int j = 0; j < 4; j++)
                    {
                        m_playerJoinedText[j].SetActive(false);
                        m_bPlayersActive[j] = false;
                    }
                    m_nPlayerCount = 0;

                    // switch to main menu screen
                    m_playerJoinScreen.SetActive(false);
                    m_mainMenuScreen.SetActive(true);
                }
            }
        }
	}

    private void OnLevelWasLoaded(int level)
    {
        if (level == 0)
            Destroy(gameObject);
    }
}