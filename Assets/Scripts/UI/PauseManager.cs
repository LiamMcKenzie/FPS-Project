using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    private bool isPaused = false;
    private bool inSettingsMenu = false; //Pressing escape while in the settings menu should not unpause the game, and should instead close the settings menu

    
    public GameObject pauseMenuObject;
    public GameObject settingsMenuObject;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(isPaused) //if game is paused
            {
                if(settingsMenuObject.activeSelf) //if settings menu is open, close it
                {
                    settingsMenuObject.SetActive(false);
                    pauseMenuObject.SetActive(true);
                }else //if settings menu is not open, unpause the game
                {
                    UnPauseGame();
                }
            }
            else
            {
                PauseGame();
            }
            
        }

        //pauseMenuObject.SetActive(GameManager.instance.IsPaused());
    }


    public void PauseGame()
    {
        
        Time.timeScale = 0; //this doesn't stop all scripts from running
        isPaused = true;
        pauseMenuObject.SetActive(true);

    }

    public void UnPauseGame()
    {
        Time.timeScale = 1; //timescale is persistent, so make sure to unpause when changing scenes. 
        isPaused = false;
        pauseMenuObject.SetActive(false);
    }

    public bool ReturnIsPaused()
    {
        return isPaused;
    }
}
