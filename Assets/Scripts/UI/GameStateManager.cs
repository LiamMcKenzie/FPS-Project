using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Setup,
    GamePlay,
    GameOver,
    GameWin
}

public class GameStateManager : MonoBehaviour
{
    private GameState gameState;
    private bool canControlPlayer;

    public void SetGameState(GameState newState)
    {
        gameState = newState;
    }

    public GameState ReturnState()
    {
        return gameState;
    }

    

    public bool ReturnPlayerControl()
    {   
        canControlPlayer = GameManager.instance.GetGameState() == GameState.GamePlay;
        return canControlPlayer;
    }
}
