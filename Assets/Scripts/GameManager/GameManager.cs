using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameState gameState;

    

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
}
