using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponSkin", menuName = "WeaponSkin")]
public class WeaponSkin : ScriptableObject
{
    public Sprite skinImage;
    public List<Material> materialGroup1;
    public List<Material> materialGroup2;
}