using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public EnemyType enemyType;
    private LayerMask layerMask;
    private GameObject player;

    private float damage;
    private float attackSpeed;

    private float shotCooldown = 0f;
    private bool canAttack = false;
    void Start()
    {
        layerMask = LayerMask.GetMask("World");
        player = GameObject.FindGameObjectWithTag("Player");
        AssignEnemyStats();
    }
    

    void Update()
    {
        CheckLineOfSight();
        AttackCooldown();

        if (canAttack && CheckLineOfSight())
        {
            Attack();
        }
    }

    void AssignEnemyStats()
    {
        damage = GameManager.instance.GetEnemyStat("Damage", enemyType);
        attackSpeed = GameManager.instance.GetEnemyStat("AttackSpeed", enemyType);
    }

    //TODO: attack cooldown, check for los and cooldown before attacking, spawn projectile

    bool CheckLineOfSight()
    {
        RaycastHit hit;
        Vector3 direction = player.transform.position - transform.position;
        if (Physics.Raycast(transform.position, direction, out hit, Mathf.Infinity, layerMask))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                return true;
            }
        }
        return false;
    }

    void Attack()
    {
        switch (enemyType)
        {
            case EnemyType.Melee:

                break;

            case EnemyType.Ranged:

                break;

            case EnemyType.Fast:

                break;

            case EnemyType.Boss:
                break;

            default:

                break;
        }
    }

    void AttackCooldown()
    {
        if (shotCooldown > 0)
        {
            canAttack = false;
            shotCooldown -= Time.deltaTime;
        }else
        {
            canAttack = true;
            shotCooldown = 0;
        }
    }
}
