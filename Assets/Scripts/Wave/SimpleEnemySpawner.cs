using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemySpawner : MonoBehaviour
{

    [System.Serializable]
    public struct EnemySquad
    {
        public int count;
        public GameObject enemyPrefab;
        public BoxCollider spawnArea;
        public EnemySquad(int count, GameObject enemyPrefab, BoxCollider spawnArea)
        {
            this.count = count;
            this.enemyPrefab = enemyPrefab;
            this.spawnArea = spawnArea;
        }
    }

    [SerializeField] public List<EnemySquad> enemies = new List<EnemySquad>();
    [SerializeField] public List<EnemySquad> remainingEnemies = new List<EnemySquad>();


    public void SpawnEnemies()
    {
        //spawns all enemies in the list
        for (int i = 0; i < enemies.Count; i++) //loops through all "squads".
        {
            for (int j = 0; j < enemies[i].count; j++) //loops through all enemies in the squad
            {
                GameObject enemy = Spawn(enemies[i].enemyPrefab, GetRandomPosInBounds(enemies[i].spawnArea));
                enemy.GetComponent<EnemyNavigation>().pathParent = enemies[i].spawnArea.transform.gameObject; //sets the path parent for the enemy to the spawn area
            }
        }
    }

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
}
