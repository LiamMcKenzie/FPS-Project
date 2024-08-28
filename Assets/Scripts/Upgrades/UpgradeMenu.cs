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
        scrollRect = gameObject.GetComponent<ScrollRect>();

        foreach (string upgradeName in GameManager.instance.GetUpgradeNames())
        {
            GameObject upgrade = Instantiate(UpgradePrefab, scrollRect.content);
            UpgradeObject upgradeObject = upgrade.GetComponent<UpgradeObject>();

            upgradeObject.upgradeName = upgradeName;
            //upgrade.GetComponentInChildren<Text>().text = upgradeName;
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
