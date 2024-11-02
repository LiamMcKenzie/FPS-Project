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
                SaveManager.instance.pistolSkin = skinIndex;
                break;

            case WeaponType.Shotgun:
                SaveManager.instance.shotgunSkin = skinIndex;
                break;

            default:
                break;
        }
    }
}
