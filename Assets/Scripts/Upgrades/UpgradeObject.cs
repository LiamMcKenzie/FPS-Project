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
    public int upgradeIndex;
    public List<GameObject> upgradeTicks = new List<GameObject>();
    public Button buyButton;
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

        buyButton = gameObject.GetComponentInChildren<Button>();

        Transform tickParent = gameObject.transform.Find("Ticks"); //searches this object for the child named "Ticks"
        for (int i = 0; i < maxTicks; i++)
        {
            GameObject tick = new GameObject("Tick" + i); //create new game object
            tick.AddComponent<RawImage>(); //add sprite renderer
            tick.GetComponent<RawImage>().color = Color.gray;
            tick.transform.SetParent(tickParent); //adds new tick image to the parent

            tick.transform.localScale = new Vector3(1, 1, 1); //sets scale to 1 (otherwise it spawns at a weird size)
            upgradeTicks.Add(tick);
        }

        //Upgrade Text
        textObject = gameObject.GetComponentInChildren<TMP_Text>();
        textObject.text = upgradeName;
    }

    void Update()
    {
        if(currentTicks == maxTicks)
        {
            foreach (var tick in upgradeTicks)
            {
                tick.GetComponent<RawImage>().color = Color.yellow;
            }
        }

        buyButton.interactable = GameManager.instance.GetUpgradePoints() > 0; //buy button is only interactable if there are more than 0 upgrade points
    }

    public void BuyUpgrade()
    {
        if(currentTicks < maxTicks && GameManager.instance.GetUpgradePoints() > 0)
        {
            upgradeTicks[currentTicks].GetComponent<RawImage>().color = Color.cyan; //sets the tick image color
            currentTicks++;
            GameManager.instance.DecreaseUpgradePoints();
            GameManager.instance.IncreaseUpgrade(upgradeIndex);
        }

    }
}
