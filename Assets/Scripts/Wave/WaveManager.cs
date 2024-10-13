using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public int currentWave = 0; 
    public List<Wave> waves = new List<Wave>();
    public void WaveComplete()
    {
        //TODO: include check if this is last wave. if so set state to GameWin, else move to next wave
        if(GameManager.instance.GetGameState() == GameState.GamePlay)
        {
            if(GameManager.instance.GetEnemyList().Count == 0)
            {
                if(IsLastWave())
                {
                    GameManager.instance.UpdateGameState(GameState.GameWin);
                }
                else
                {
                    currentWave++;
                    //CompletedWaveCount.waveCount++;
                    SaveManager.instance.waveCount++;
                    SaveManager.instance.SaveToFile();
                    GameManager.instance.OpenUpgradeMenu();
                }
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

    public bool IsLastWave()
    {
        return currentWave == waves.Count - 1;
    }

    public Wave ReturnWave()
    {
        return waves[currentWave];
    }

    public int ReturnWaveNumber()
    {
        return currentWave + 1; //(wave 1 is actually wave 0), I want to return a human readable number
    }

    public int ReturnWaveCount()
    {
        return waves.Count;
    }
}