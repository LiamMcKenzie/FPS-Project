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
        //TODO: instantiate upgrade object from list of upgrades.
        //TODO: create gamemanager function for getting upgrade list.
        foreach (string upgradeName in GameManager.instance.GetUpgradeNames())
        {
            GameObject upgrade = Instantiate(UpgradePrefab, scrollRect.content);
            //upgrade.GetComponentInChildren<Text>().text = upgradeName;
            upgrade.GetComponentInChildren<TMP_Text>().text = upgradeName;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
