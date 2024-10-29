using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSkinIndex : MonoBehaviour
{
    public int skinIndex;
    public WeaponType weaponType;
    
    public void ChangeSkin()
    {
        switch (weaponType)
        {
            case WeaponType.Pistol:
                PlayerData.instance.pistolSkinIndex = skinIndex;
                break;

            case WeaponType.Shotgun:
                PlayerData.instance.shotgunSkinIndex = skinIndex;
                break;

            default:
                PlayerData.instance.pistolSkinIndex = skinIndex;
                break;
        }
    }
}
