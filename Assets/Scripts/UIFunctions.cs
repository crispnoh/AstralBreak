using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFunctions : MonoBehaviour
{
    //Game objects
    GameObject pauseMenu;
    GameObject settingsMenu;
    GameObject deathScreen;

    //variables yk
    public bool gamePaused = false;

    void Start()
    {
        //if it says one of the below is not set to an instance of an object, first set the pause or settings menu to active in the editor (click on it and check the box to the left of the name)
        settingsMenu = GameObject.Find("SettingsMenu");
        pauseMenu = GameObject.Find("PauseMenu");
        deathScreen = GameObject.Find("DeathScreen");
        settingsMenu.SetActive(false);
        pauseMenu.SetActive(false);
        deathScreen.SetActive(false);
    }

    public void die()
    {
        Time.timeScale = 0f;
        deathScreen.SetActive(true);
    }

    public void pauseGame()
    {
        //Debug.Log("paused");
        gamePaused = !gamePaused;

        if (gamePaused) 
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f; 
        }
        else 
        {
            settingsMenu.SetActive(false);
            pauseMenu.SetActive(false);
            Time.timeScale = 1f; 
        }
    }

    public void RestartGame()
    {
        //restarts the level. respawn enemies, reset spawn point, level assets, controls, etc 
        Time.timeScale = 1f;
        deathScreen.SetActive(false);
    }

    public void exitGame()
    {
        Application.Quit();
    }
}
