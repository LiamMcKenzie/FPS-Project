using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    private bool isPaused = false;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(isPaused)
            {
                UnPauseGame();
            }
            else
            {
                PauseGame();
            }
            
        }
    }


    public void PauseGame()
    {
        
        Time.timeScale = 0; //this doesn't stop all scripts from running
        isPaused = true;

    }

    public void UnPauseGame()
    {
        Time.timeScale = 1; //timescale is persistent, so make sure to unpause when changing scenes. 
        isPaused = false;
    }

    public bool ReturnIsPaused()
    {
        return isPaused;
    }
}
