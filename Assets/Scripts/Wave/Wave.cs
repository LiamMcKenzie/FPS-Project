using UnityEngine;
using System.Collections.Generic;

public enum SpawnArea
{
    Left,
    Right
}

[CreateAssetMenu(fileName = "NewWave", menuName = "Wave", order = 1)]
public class Wave : ScriptableObject
{
    [System.Serializable]
    public struct EnemySquad
    {
        public int count;
        public GameObject enemyPrefab;
        public SpawnArea spawnArea;
        public EnemySquad(int count, GameObject enemyPrefab, SpawnArea spawnArea)
        {
            this.count = count;
            this.enemyPrefab = enemyPrefab;
            this.spawnArea = spawnArea;
        }
    }

    [SerializeField] public List<EnemySquad> enemies = new List<EnemySquad>();
    // Add your variables and properties here

    // Add your methods and functions here
}