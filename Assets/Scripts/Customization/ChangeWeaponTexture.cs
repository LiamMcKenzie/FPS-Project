using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeWeaponTexture : MonoBehaviour
{
    public List<Material> newMaterials;
    public List<Material> newMaterials2;
    // Start is called before the first frame update
    void Start()
    {
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
                childRenderers[i].materials = newMaterials.ToArray();
            }
            else
            {
                // Apply each new material to the renderer
                childRenderers[i].materials = newMaterials2.ToArray();
            }
        }
    }

    // Call this method if you want to dynamically change the materials at runtime
    public void SetNewMaterials(List<Material> materials)
    {
        newMaterials = materials;
        ChangeMaterialsOfChildren();
    }
}
