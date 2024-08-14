using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    [SerializeField] private GameStateManager gameStateManager;
    [SerializeField] private SimpleEnemySpawner enemySpawner;
    
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
}
