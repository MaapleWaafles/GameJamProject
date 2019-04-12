using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Button startButton;
    public GameObject mainMenu;
    public GameObject playerJoin;

    private void Awake()
    {
        startButton.Select();
    }

    public void StartButton()
    {
        // display player join screen
        mainMenu.SetActive(false);
        playerJoin.SetActive(true);
    }

    public void QuitButton()
    {
        // quit application
        Application.Quit();
    }
}