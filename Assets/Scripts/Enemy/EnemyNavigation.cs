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
    [HideInInspector] public GameObject pathParent;
    private List<BoxCollider> checkpoints = new List<BoxCollider>();
    private int targetIndex = 0;
    private NavMeshAgent agent;
    public EnemyType enemyType;

    void Start()
    {
        LoadPathData(pathParent);

        //sets the first destination
        agent = GetComponent<NavMeshAgent>();       
        agent.SetDestination(GetRandomPosInBounds(checkpoints[targetIndex]));

        agent.speed = GameManager.instance.GetEnemyStat("MoveSpeed", enemyType);
    }

    void Update()
    {
        //if the agent has reached the target, then set the next destination
        if (agent.remainingDistance < 0.5f && targetIndex < checkpoints.Count - 1)
        {
            targetIndex++;
            agent.SetDestination(GetRandomPosInBounds(checkpoints[targetIndex]));
        }
    }

    /// <summary>
    /// Finds the checkpoint children from the path parent
    /// </summary>
    /// <param name="pathParent"></param>
    void LoadPathData(GameObject pathParent)
    {
        GameObject[] checkpointChildren = GetChildrenWithTag(pathParent, "Checkpoint");
        foreach (var child in checkpointChildren)
        {
            checkpoints.Add(child.GetComponent<BoxCollider>());
        }
    }

    /// <summary>
    /// returns all children of a parent object with a given tag. NOTE: only checks the first level of children
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="tag"></param>
    /// <returns></returns>
    GameObject[] GetChildrenWithTag(GameObject parent, string tag)
    {
        List<GameObject> children = new List<GameObject>();
        foreach (Transform child in parent.transform)
        {
            if (child.CompareTag(tag))
            {
                children.Add(child.gameObject);
            }
        }
        return children.ToArray();
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
