using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{

    public void PlayGame()
    {
        SceneManager.LoadSceneAsync("Lobby");
    }

    public void PlayCredits()
    {
        SceneManager.LoadSceneAsync("Credits");
    }

    public void QuitGame()
    {
        Debug.Log("I quit the game");
        Application.Quit();
    }

}
