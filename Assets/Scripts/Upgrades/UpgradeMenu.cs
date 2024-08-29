using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeMenu : MonoBehaviour
{
    public GameObject UpgradePrefab;
    private ScrollRect scrollRect;
    // Start is called before the first frame update
    void Start()
    {
        //finds the scrollrect component and finds the upgrade sections.
        scrollRect = gameObject.GetComponent<ScrollRect>();
        Transform playerSection = scrollRect.content.Find("Player");
        Transform pistolSection = scrollRect.content.Find("Pistol");
        Transform shotgunSection = scrollRect.content.Find("Shotgun");


        //creates all upgrade objects
        for (int i = 0; i < GameManager.instance.GetUpgradeCount(); i++)
        {
            //Gets the correct upgrade section (player, pistol, shotgun)
            Transform upgradeSectionParent;

            switch (GameManager.instance.GetUpgradeSection(i))
            {
                case UpgradeSection.Player:
                    upgradeSectionParent = playerSection;
                    break;

                case UpgradeSection.Pistol:
                    upgradeSectionParent = pistolSection;
                    break;

                case UpgradeSection.Shotgun:
                    upgradeSectionParent = shotgunSection;
                    break;

                default:
                    upgradeSectionParent = playerSection;
                    break;
            }

            GameObject upgrade = Instantiate(UpgradePrefab, upgradeSectionParent); //Instantiates the upgrade object

            UpgradeObject upgradeObject = upgrade.GetComponent<UpgradeObject>();

            //Assigns the correct values, these values are applied in the UpgradeObject.cs start function
            upgradeObject.upgradeName = GameManager.instance.GetUpgradeName(i);
        }

        // foreach (string upgradeName in GameManager.instance.GetUpgradeNames())
        // {
            
        //     GameObject upgrade = Instantiate(UpgradePrefab, scrollRect.content);
        //     UpgradeObject upgradeObject = upgrade.GetComponent<UpgradeObject>();

        //     upgradeObject.upgradeName = upgradeName;
        //     //upgrade.GetComponentInChildren<Text>().text = upgradeName;
            
        // }
    }
}
