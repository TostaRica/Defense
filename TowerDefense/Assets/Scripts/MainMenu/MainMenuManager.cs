using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
  
    public void StartGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void LoadSettings()
    {
        SceneManager.LoadScene("SettingsScene");
    }
    public void LoadCredits()
    {
        SceneManager.LoadScene("CreditsScene");
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void closeGame()
    {
        Application.Quit();
    }
}
