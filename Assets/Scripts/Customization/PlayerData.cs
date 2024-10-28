using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData instance;
    public int playerCurrency;
    public int pistolSkinIndex;
    public int shotgunSkinIndex;

    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
