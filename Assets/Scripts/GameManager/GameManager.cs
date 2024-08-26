using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UpgradeManager; 

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    [SerializeField] private GameStateManager gameStateManager;
    [SerializeField] private SimpleEnemySpawner enemySpawner;
    
    [SerializeField] private Restart restart;
    [SerializeField] private PauseManager pauseManager;
    [SerializeField] private UpgradeManager upgradeManager;
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void StartWave()
    {
        UpdateGameState(GameState.GamePlay);
        enemySpawner.SpawnEnemies(); //This needs to be changed to use remaining enemies

    }

    public GameState GetGameState()
    {
        return gameStateManager.ReturnState();
    }

    public void UpdateGameState(GameState newState)
    {
        gameStateManager.SetGameState(newState);
    }

    public bool CanControlPlayer()
    {
        return gameStateManager.ReturnPlayerControl() && pauseManager.ReturnIsPaused() == false;
    }

    public void RestartWave()
    {
        restart.ReloadScene();
    }

    public List<GameObject> GetEnemyList()
    {
        return enemySpawner.ReturnSpawnedEnemies();
    }

    //used to add or remove enemies
    public void RemoveEnemyFromList(GameObject enemy)
    {
        enemySpawner.RemoveEnemyFromList(enemy); 
    }

    public bool IsPaused()
    {
        return pauseManager.ReturnIsPaused();
    }

    //UPGRADES
    public List<Upgrade> GetUpgrades()
    {
        return upgradeManager.ReturnUpgrades();
    }

}
