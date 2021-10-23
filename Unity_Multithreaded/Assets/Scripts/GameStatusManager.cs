using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameStatusManager : MonoBehaviour
{
    public GameObject WinInfoPanel;
    public GameObject LoseInfoPanel;

    private LogManager logManager;

    private void Start()
    {
        WinInfoPanel.SetActive(false);
        LoseInfoPanel.SetActive(false);

        logManager = LogManager._instance;
    }

    public void ManuallyQuitGame()
    {
        // since for all the cases we want to go back to main menu we will call this method,
        // we join all threads here
        logManager.JoinAllSideThreads();

        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }

    public void Win()
    {
        WinInfoPanel.SetActive(true);
        StartCoroutine("GoBackToMenu");
    }

    public void Lose()
    {
        LoseInfoPanel.SetActive(true);
        StartCoroutine("GoBackToMenu");
    }


    IEnumerator GoBackToMenu()
    {
        yield return new WaitForSeconds(2f);
        ManuallyQuitGame();
    }
}
