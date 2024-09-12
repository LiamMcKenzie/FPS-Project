using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    public float damage;
    public GameObject deathEffect;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit");
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealth>().TakeDamage(damage);
            if (deathEffect != null)
            {
                Instantiate(deathEffect, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if(GameManager.instance.GetGameState() != GameState.GamePlay) //deletes the projectile when the wave or game ends
        {
            Destroy(gameObject);
        }
    }
}
