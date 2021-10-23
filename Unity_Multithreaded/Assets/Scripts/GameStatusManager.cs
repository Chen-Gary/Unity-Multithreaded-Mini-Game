using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameStatusManager : MonoBehaviour
{
    public GameObject WinInfoPanel;
    public GameObject LoseInfoPanel;

    private void Start()
    {
        WinInfoPanel.SetActive(false);
        LoseInfoPanel.SetActive(false);
    }

    public void ManuallyQuitGame()
    {
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
