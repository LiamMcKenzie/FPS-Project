using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Setup,
    GamePlay,
    GameOver
}

public class GameStateManager : MonoBehaviour
{
    private GameState gameState;

    public void SetGameState(GameState newState)
    {
        gameState = newState;
    }

    public GameState ReturnState()
    {
        return gameState;
    }
}
