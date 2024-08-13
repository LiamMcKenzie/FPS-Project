using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health = 100;
    
    void Update()
    {
        if (health <= 0)
        {
            Die();
        }
    }

    public void TakeDamage(int damage)
    {

        DamageNumberSpawner.Instance.SpawnDamageNumber(this.gameObject, damage);
        health -= damage;
    }

    public void Die()
    {
        //play particle effects here
        //Destroy(gameObject);
        gameObject.SetActive(false);
    }
}
