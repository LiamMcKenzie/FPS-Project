using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemySpawner : MonoBehaviour
{

    public List<Wave> waves = new List<Wave>();
    [SerializeField] public List<GameObject> spawnedEnemies = new List<GameObject>();
    
    [Header("Spawn Areas")]
    public BoxCollider rightSpawnArea;
    public BoxCollider leftSpawnArea;

    /// <summary>
    /// Start spawning enemies for the current wave
    /// </summary>
    public void SpawnEnemies(int waveIndex)
    {
        //spawns all enemies in the list
        for (int i = 0; i < waves[waveIndex].enemies.Count; i++) //loops through all "squads".
        {
            for (int j = 0; j < waves[waveIndex].enemies[i].count; j++) //loops through all enemies in the squad
            {
                GameObject enemy = Spawn(waves[waveIndex].enemies[i].enemyPrefab, GetRandomPosInBounds(GetSpawnPosition(waves[waveIndex].enemies[i].spawnArea)));
                spawnedEnemies.Add(enemy);
                enemy.GetComponent<EnemyNavigation>().pathParent = GetSpawnPosition(waves[waveIndex].enemies[i].spawnArea).gameObject; //sets the path parent for the enemy to the spawn area
            }
        }
    }

    // public void SpawnEnemies()
    // {
    //     //spawns all enemies in the list
    //     for (int i = 0; i < enemies.Count; i++) //loops through all "squads".
    //     {
    //         for (int j = 0; j < enemies[i].count; j++) //loops through all enemies in the squad
    //         {
    //             GameObject enemy = Spawn(enemies[i].enemyPrefab, GetRandomPosInBounds(enemies[i].spawnArea));
    //             spawnedEnemies.Add(enemy);
    //             enemy.GetComponent<EnemyNavigation>().pathParent = enemies[i].spawnArea.transform.gameObject; //sets the path parent for the enemy to the spawn area
    //         }
    //     }
    // }

    /// <summary>
    /// returns a random position within the bounds of a box collider. NOTE: always spawns at the bottom of the box collider
    /// </summary>
    /// <param name="boxCollider"></param>
    /// <returns></returns>
    public Vector3 GetRandomPosInBounds(BoxCollider boxCollider)
    {
        Bounds bounds = boxCollider.bounds;
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            bounds.min.y, //always spawn at bottom of box collider
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }

    /// <summary>
    /// spawns a prefab at a given position
    /// </summary>
    /// <param name="prefab"></param>
    /// <param name="spawnPosition"></param>
    /// <returns></returns>
    GameObject Spawn(GameObject prefab, Vector3 spawnPosition)
    {
        return Instantiate(prefab, spawnPosition, Quaternion.identity);
    }

    public List<GameObject> ReturnSpawnedEnemies()
    {
        return spawnedEnemies;
    }

    public void RemoveEnemyFromList(GameObject enemy)
    {
        if(spawnedEnemies.Contains(enemy))
        {
            spawnedEnemies.Remove(enemy);
        }else{
            Debug.LogWarning("Tried to remove enemy from spawnedEnemies list, but enemy was not found in list");
        }
    }

    public BoxCollider GetSpawnPosition(SpawnArea spawnArea)
    {
        switch (spawnArea)
        {
            case SpawnArea.Left:
                return leftSpawnArea;
            case SpawnArea.Right:
                return rightSpawnArea;
            default:
                return null;
        }
    }
}
