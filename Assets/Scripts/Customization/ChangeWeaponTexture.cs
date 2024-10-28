using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ChangeWeaponTexture : MonoBehaviour
{
    // public List<Material> newMaterials;
    // public List<Material> newMaterials2;


    public WeaponType weaponType;
    public List<WeaponSkin> weaponSkins = new List<WeaponSkin>();
    private WeaponSkin weaponSkin;
    // Start is called before the first frame update
    void Start()
    {
        switch (weaponType)
        {
            case WeaponType.Pistol:
                weaponSkin = weaponSkins[PlayerData.instance.pistolSkinIndex];
                break;

            case WeaponType.Shotgun:
                weaponSkin = weaponSkins[PlayerData.instance.shotgunSkinIndex];
                break;

            default:
                weaponSkin = weaponSkins[PlayerData.instance.pistolSkinIndex];
                break;

        }

        ChangeMaterialsOfChildren();
    }

    public void ChangeMaterialsOfChildren()
    {
        // Get all child renderers
        Renderer[] childRenderers = GetComponentsInChildren<Renderer>();

        for (int i = 0; i < childRenderers.Length; i++)
        {
            
            if (i == 0)
            {
                // Apply each new material to the renderer
                childRenderers[i].materials = weaponSkin.materialGroup1.ToArray();
            }
            else
            {
                // Apply each new material to the renderer
                childRenderers[i].materials = weaponSkin.materialGroup2.ToArray();
            }
        }
    }
}
