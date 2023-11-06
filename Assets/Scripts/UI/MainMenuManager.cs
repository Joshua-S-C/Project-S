using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public string LevelName = "Stage";
    public void OpenStage()
    {
        SceneManager.LoadScene(LevelName);
    }
    public string SettingsMenu = "Settings Menu";
    public void OpenSettings()
    {
        SceneManager.LoadScene(SettingsMenu);
    }
    public string MainMenu = "Main Menu";
    public void OpenMainMenu()
    {
        SceneManager.LoadScene(MainMenu);
    }
}
