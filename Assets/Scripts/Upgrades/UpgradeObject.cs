using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// This object controls the functionality of each upgrade menu object.
/// </summary>
/// <remarks>
/// This script should be attached to the upgrade object parent, there should be one for each upgrade.
/// prefab setup:
/// upgrade object parent (attach script here)
///     upgrade name text
///     background image
///     ticks
///         tick images go here (created at runtime)
///     buy button
///         button components
/// </remarks>
public class UpgradeObject : MonoBehaviour
{
    public List<GameObject> upgradeTicks = new List<GameObject>();
    //private GameObject tickParent;
    public int maxTicks;
    public int currentTicks = 0;

    private TMP_Text textObject;
    [HideInInspector] public string upgradeName;

    // Start is called before the first frame update
    void Start()
    {
        //Tick Images
        //This is a way of getting all the tick game objects at runtime without using the inspector.
        Transform tickParent = gameObject.transform.Find("Ticks"); //searches this object for the child named "Ticks"
        for (int i = 0; i < maxTicks; i++)
        {
            GameObject tick = new GameObject("Tick" + i); //create new game object
            tick.AddComponent<RawImage>(); //add sprite renderer
            tick.transform.SetParent(tickParent); //adds new tick image to the parent

            tick.transform.localScale = new Vector3(1, 1, 1); //sets scale to 1 (otherwise it spawns at a weird size)

        }
        foreach (Transform child in tickParent) //gets all the children and adds to list
        {
            upgradeTicks.Add(child.gameObject);
        }

        //Upgrade Text
        textObject = gameObject.GetComponentInChildren<TMP_Text>();
        textObject.text = upgradeName;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
