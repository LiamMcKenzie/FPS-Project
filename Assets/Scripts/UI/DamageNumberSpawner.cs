using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageNumberSpawner : MonoBehaviour
{
    public static DamageNumberSpawner Instance;
    [SerializeField] private GameObject damageNumberPrefab;
    [SerializeField] private Transform canvasTransform;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("more than one instance of DamageNumberSpawner, the second one won't be assigned");
            return;
        }
        Instance = this;
    }

    public void SpawnDamageNumber(GameObject enemyParent, int damage)
    {
        GameObject damageNumber = Instantiate(damageNumberPrefab, enemyParent.transform.position, Quaternion.identity, canvasTransform);

        damageNumber.GetComponent<DamageNumber>().targetObject = enemyParent.transform;
        damageNumber.GetComponent<DamageNumber>().damage = damage;
    }
}