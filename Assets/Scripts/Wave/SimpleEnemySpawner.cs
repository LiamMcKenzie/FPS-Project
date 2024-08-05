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
        public Transform spawnPosition;
        public EnemySquad(int count, GameObject enemyPrefab, Transform spawnPosition)
        {
            this.count = count;
            this.enemyPrefab = enemyPrefab;
            this.spawnPosition = spawnPosition;
        }
    }

    [SerializeField] public List<EnemySquad> enemies = new List<EnemySquad>();

    void Spawn(GameObject prefab, Vector3 spawnPosition)
    {
        Instantiate(prefab, spawnPosition, Quaternion.identity);
    }
}
