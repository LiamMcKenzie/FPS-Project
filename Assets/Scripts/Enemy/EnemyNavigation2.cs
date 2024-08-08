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

public class EnemyNavigation2 : MonoBehaviour
{
    public GameObject[] targets;
    public int targetIndex = 0;
    private NavMeshAgent agent;

    void Start()
    {
        targets = GameObject.FindGameObjectsWithTag("Goal");
        agent = GetComponent<NavMeshAgent>();       
        agent.SetDestination(targets[targetIndex].transform.position);     
    }

    void Update()
    {
        if (agent.remainingDistance < 0.5f && targetIndex < targets.Length - 1)
        {
            targetIndex++;
            agent.SetDestination(targets[targetIndex].transform.position);
        }
    }
}
