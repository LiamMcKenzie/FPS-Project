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
    public BoxCollider[] checkpoints;
    private Vector3[] targets;
    public int targetIndex = 0;
    private NavMeshAgent agent;

    void Start()
    {
        
        //targets = GameObject.FindGameObjectsWithTag("Goal");
        agent = GetComponent<NavMeshAgent>();       
        agent.SetDestination(GetRandomPosInBounds(checkpoints[targetIndex]));
        //agent.SetDestination(targets[targetIndex].transform.position);     
    }

    void Update()
    {
        //if the agent has reached the target, then set the next destination
        if (agent.remainingDistance < 0.5f && targetIndex < checkpoints.Length - 1)
        {
            targetIndex++;
            agent.SetDestination(GetRandomPosInBounds(checkpoints[targetIndex]));
            //agent.SetDestination(targets[targetIndex].transform.position);
        }
    }

    /// <summary>
    /// returns a random position within the bounds of a box collider. NOTE: y axis is set to the bottom of the box
    /// </summary>
    /// <param name="boxCollider"></param>
    /// <returns></returns>
    public Vector3 GetRandomPosInBounds(BoxCollider boxCollider)
    {
        Bounds bounds = boxCollider.bounds;
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            bounds.min.y, //always choose bottom of box collider
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }
}
