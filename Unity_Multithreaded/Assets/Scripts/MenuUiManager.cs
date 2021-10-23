using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuUiManager : MonoBehaviour
{
    public void OnClickExit()
    {
        Application.Quit();
    }

    public void OnClickStartGame()
    {
        SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
    }
}
