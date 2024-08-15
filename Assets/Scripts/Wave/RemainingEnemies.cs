using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemainingEnemies : MonoBehaviour
{
    private List<GameObject> enemiesLeft = new List<GameObject>();

    void Update()
    {
        //if all enemies have died move to next wave and change state to setup
        if(GameManager.instance.GetEnemyList().Count == 0) 
        {
            GameManager.instance.UpdateGameState(GameState.Setup);
        }
    }
}