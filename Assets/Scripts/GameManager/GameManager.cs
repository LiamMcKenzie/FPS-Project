using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    //Gets upgrade count, and runs functions to get specific values using an ID.
    public int GetUpgradeCount()
    {
        return upgradeManager.ReturnListSize();
    }

    public int GetUpgradeLevelCount(int index)
    {
        return upgradeManager.ReturnUpgradeLevels(index);
    }

    public string GetUpgradeName(int index)
    {
        return upgradeManager.ReturnUpgradeName(index);
    }
    public UpgradeSection GetUpgradeSection(int index)
    {
        return upgradeManager.ReturnUpgradeSection(index);
    }

    public int GetUpgradeProgress(int index)
    {
        return upgradeManager.ReturnUpgradeProgress(index);
    }
    public List<float> GetUpgradeValues(int index) //keep in mind this uses index, so speed value is currently at index 1, but if i add more upgrades it will need to be adjusted.
    {
        return upgradeManager.ReturnUpgradeValues(index);
    }

    public float GetUpgradeValue(int index) //gets the current value for a specific upgrade
    {
        return GetUpgradeValues(index)[GetUpgradeProgress(index)];
    }

    public int GetUpgradePoints()
    {
        return upgradeManager.GetUpgradePoints();
    }

    public void DecreaseUpgradePoints()
    {
        upgradeManager.upgradePoints--;
    }

    public void IncreaseUpgrade(int index)
    {
        upgradeManager.IncreaseProgress(index);
    }

}
