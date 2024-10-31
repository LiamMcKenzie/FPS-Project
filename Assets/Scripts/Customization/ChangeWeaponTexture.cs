using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ChangeWeaponTexture : MonoBehaviour
{
    // public List<Material> newMaterials;
    // public List<Material> newMaterials2;


    public WeaponType weaponType;
    public List<WeaponSkin> weaponSkins = new List<WeaponSkin>();
    //private WeaponSkin weaponSkin;
    private int skinIndex;
    // Start is called before the first frame update
    void Start()
    {
        ChangeMaterials();
    }

    void Update()
    {
        CheckIndex();
    }

    public void CheckIndex()
    {
        int dataIndex = 0; //initialize to 0

        switch (weaponType) //sets dataindex to either pistol or shotgun skin index
        {
            case WeaponType.Pistol:
                dataIndex = PlayerData.instance.pistolSkinIndex;
                break;

            case WeaponType.Shotgun:
                dataIndex = PlayerData.instance.shotgunSkinIndex;
                break;

            default:
                break;
        }

        //check if dataindex is out of bounds
        if(dataIndex < 0)
        {
            dataIndex = 0;
        }
        else if(dataIndex >= weaponSkins.Count)
        {
            dataIndex = weaponSkins.Count - 1;
        }


        //check if the current skin has changed
        if(skinIndex != dataIndex)
        {
            skinIndex = dataIndex;
            ChangeMaterials();
        }
    } 

    public void ChangeMaterials()
    {
        // Get all child renderers
        Renderer[] childRenderers = GetComponentsInChildren<Renderer>();

        for (int i = 0; i < childRenderers.Length; i++)
        {
            
            if (i == 0)
            {
                // Apply each new material to the renderer
                childRenderers[i].materials = weaponSkins[skinIndex].materialGroup1.ToArray();
            }
            else
            {
                // Apply each new material to the renderer
                childRenderers[i].materials = weaponSkins[skinIndex].materialGroup2.ToArray();
            }
        }
    }
}
