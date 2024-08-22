using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is responsible for displaying the pause menu when the game is paused.
/// </summary>
public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuObject;
    void Update()
    {
        pauseMenuObject.SetActive(GameManager.instance.IsPaused());
    }
}
