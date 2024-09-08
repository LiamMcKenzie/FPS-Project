using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EnemyAttack : MonoBehaviour
{
    public EnemyType enemyType;
    private LayerMask layerMask;
    private GameObject player;
    void Start()
    {
        layerMask = LayerMask.GetMask("World");
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        CheckLineOfSight();
    }

    //TODO: attack cooldown, check for los and cooldown before attacking, spawn projectile

    void CheckLineOfSight()
    {
        RaycastHit hit;
        Vector3 direction = player.transform.position - transform.position;
        if (Physics.Raycast(transform.position, direction, out hit, Mathf.Infinity, layerMask))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                Debug.Log("Player in sight");
            }
        }
    }
}
