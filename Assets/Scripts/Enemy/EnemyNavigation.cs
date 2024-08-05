using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// This class is responsible for the enemy navigation.
/// </summary>
/// <remarks>
/// Created with help from the following tutorial:
/// https://www.youtube.com/watch?v=u2EQtrdgfNs
/// </remarks>

public class EnemyNavigation : MonoBehaviour
{
    private Transform target;
    private NavMeshAgent agent;

    void Start()
    {
        GameObject[] goals;
        goals = GameObject.FindGameObjectsWithTag("Goal");
        target = goals[0].transform;
        
        agent = GetComponent<NavMeshAgent>();       
        agent.SetDestination(target.position);     
    }
}
