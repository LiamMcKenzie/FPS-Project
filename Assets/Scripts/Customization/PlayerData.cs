using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData instance;
    public int playerCurrency;
    //public int pistolSkinIndex;
    //public int shotgunSkinIndex;

    public List<bool> unlockedPistolSkins = new List<bool>();
    public List<bool> unlockedShotgunSkins = new List<bool>();

    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
