using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public void WaveComplete()
    {
        //TODO: include check if this is last wave. if so set state to GameWin, else move to next wave
        if(GameManager.instance.GetGameState() == GameState.GamePlay)
        {
            if(GameManager.instance.GetEnemyList().Count == 0)
            {
                GameManager.instance.UpdateGameState(GameState.GameWin);
            }
        }
    }

    void Update()
    {
        if(GameManager.instance.GetGameState() == GameState.GamePlay) //NOTE: this could be refactored to not use update. Potentially could run every time an enemy dies.
        {
            WaveComplete();
        }
    }
}