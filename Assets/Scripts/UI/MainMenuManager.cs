using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject notEnoughPlayersTextPrefab;
    private GameObject notEnoughPlayersText;
    public string LevelName = "Stage";

    
    
    public void OpenStage()
    {
        if(PlayerProfileManagerScript.GetProfileCount() >= 2)
        {
            SceneManager.LoadScene(LevelName);
        }
        else
        {
            if(notEnoughPlayersText != null)
            {
                notEnoughPlayersText.GetComponent<NotEnoughPlayersTextScript>().RestartFadeTimer();
            }
            else
            {
                notEnoughPlayersText = Instantiate(notEnoughPlayersTextPrefab, GameObject.Find("Canvas").transform);
            }
        }
        
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
    public string CharacterSelectMenu = "Character Select";
    public void OpenCharacterSelect()
    {
        SceneManager.LoadScene(CharacterSelectMenu);
    }
}
