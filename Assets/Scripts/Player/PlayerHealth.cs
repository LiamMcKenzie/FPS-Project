using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float health = 100;

    void Update()
    {
        if (health <= 0)
        {
            Die();
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
    }

    public void Die()
    {
        Debug.Log("Player died");
        GameManager.instance.UpdateGameState(GameState.GameOver); //sets gamestate to gameover
    }

    public float ReturnHealth()
    {
        return health;
    }

    public void ResetHealth()
    {
        Debug.Log("Resetting health");
        Debug.Log(GameManager.instance.GetUpgradeValue("Health", UpgradeSection.Player));
        health = GameManager.instance.GetUpgradeValue("Health", UpgradeSection.Player);
    }
}