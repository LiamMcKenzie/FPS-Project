using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health = 100;
    public EnemyType enemyType;
    public GameObject deathEffect;

    void Start()
    {
        health = GameManager.instance.GetEnemyStat("Health", enemyType);   
    }

    void Update()
    {
        if (health <= 0)
        {
            Die();
        }
    }

    public void TakeDamage(float damage)
    {

        DamageNumberSpawner.Instance.SpawnDamageNumber(this.gameObject, damage);
        health -= damage;
    }

    public void Die()
    {
        //play particle effects here
        //Destroy(gameObject);
        gameObject.SetActive(false);
        GameObject deathParticle = Instantiate(deathEffect, transform.position, Quaternion.identity);
        GameManager.instance.RemoveEnemyFromList(gameObject);
        
    }
}
