using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
