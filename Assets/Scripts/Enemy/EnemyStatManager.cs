using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    Ranged,
    Melee,
    Boss,
    Fast
}

public class EnemyStatManager : MonoBehaviour
{
    [System.Serializable]
    public struct EnemyStat
    {
        public EnemyType enemyType;
        public float health;
        public float damage;
        public float attackSpeed;
        public float moveSpeed;
        public EnemyStat(EnemyType enemyType , float health, float damage, float attackSpeed, float moveSpeed)
        {
            this.enemyType = enemyType;
            this.health = health;
            this.damage = damage;
            this.attackSpeed = attackSpeed;
            this.moveSpeed = moveSpeed;
        }
    }
    [SerializeField] public List<EnemyStat> enemyStats = new List<EnemyStat>();

    /// <summary>
    /// This function checks the index value to make sure its within range, if its not in range it picks the nearest value.
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public int GetIndexWithinRange(int index)
    {
        if(index > ReturnListSize()) //if desired index is over range, set to highest value
        {
            Debug.LogWarning("Trying to get upgrade index that is over range, setting to nearest value");
            return ReturnListSize();
        }else if (index < 0){ //if desired index is below 0, set to 0
            Debug.LogWarning("Trying to get upgrade index that is less than 0, setting to 0");
            return 0;
        }else
        {
            return index; //this value is within range
        }
    }

    public int ReturnListSize()
    {
        return enemyStats.Count;
    }

    public float GetEnemyHealth(int index)
    {
        return enemyStats[GetIndexWithinRange(index)].health;
    }

    public float GetEnemyDamage(int index)
    {
        return enemyStats[GetIndexWithinRange(index)].damage;
    }

    public float GetEnemyAttackSpeed(int index)
    {
        return enemyStats[GetIndexWithinRange(index)].attackSpeed;
    }

    public float GetEnemyMoveSpeed(int index)
    {
        return enemyStats[GetIndexWithinRange(index)].moveSpeed;
    }

    public int FindEnemyIndex(EnemyType enemyType)
    {
        for (int i = 0; i < enemyStats.Count; i++)
        {
            if(enemyStats[i].enemyType == enemyType)
            {
                return i;
            }
        }
        Debug.LogError("Enemy Type not found in list");
        return -1;
    }

    public float ReturnSpecificValue(string name, EnemyType enemyType)
    {
        int index = FindEnemyIndex(enemyType);
        switch (name)
        {
            case "Health":
                return GetEnemyHealth(index);
            case "Damage":
                return GetEnemyDamage(index);
            case "AttackSpeed":
                return GetEnemyAttackSpeed(index);
            case "MoveSpeed":
                return GetEnemyMoveSpeed(index);
            default:
                Debug.LogError("Invalid stat name input");
                return -1;
        }
    }
}