using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWinTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")) // && GameManager.instance.gameState == GameManager.GameState.GamePlay
        {
            GameManager.instance.gameState = GameState.GameOver; //sets gamestate to gameover
        }
    }
}
