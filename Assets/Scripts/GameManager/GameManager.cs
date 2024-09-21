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

    [SerializeField] private EnemyStatManager enemyStatManager;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private WaveManager waveManager;
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void OpenUpgradeMenu()
    {
        UpdateGameState(GameState.Setup);
        upgradeManager.ResetUpgradePoints();
    }

    public void StartWave()
    {
        UpdateGameState(GameState.GamePlay);
        enemySpawner.SpawnEnemies(waveManager.ReturnWave()); 
    }

    public void RestartWave()
    {
        restart.ReloadScene();
    }

    public void RefundUpgradePoints()
    {
        upgradeManager.RefundUpgradePoints(waveManager.ReturnWaveNumber());
    }

    public string GetWaveCountText()
    {
        return $"{waveManager.ReturnWaveNumber()} / {waveManager.ReturnWaveCount()}";
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

    /// <summary>
    /// This function is used by various scripts to get the current value of an upgrade-able stat. such as move speed or bullet damage.
    /// </summary>
    /// <remarks>
    /// USE: input upgrade name and upgrade type/section, returns the value for that upgrade.
    /// </remarks>
    /// <param name="name"></param>
    /// <param name="upgradeType"></param>
    /// <returns></returns>
    public float GetUpgradeValue(string name, UpgradeSection upgradeType) 
    {
        return upgradeManager.ReturnUpgradeValue(name, upgradeType);
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

    //ENEMY STATS


    /// <summary>
    /// This function is used to get a specific stat value for a type of enemy 
    /// </summary>
    /// <param name="name"></param>
    /// <param name="enemyType"></param>
    /// <returns></returns>
    public float GetEnemyStat(string name, EnemyType enemyType)
    {
        return enemyStatManager.ReturnSpecificValue(name, enemyType);
    }

    //PLAYER HEALTH
    public float GetPlayerHealth()
    {
        return playerHealth.ReturnHealth();
    }
}
