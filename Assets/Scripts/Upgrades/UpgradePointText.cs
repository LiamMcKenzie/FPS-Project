using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradePointText : MonoBehaviour
{
    private TMP_Text textObject;
    void Start()
    {
        textObject = gameObject.GetComponent<TMP_Text>();
    }

    void Update()
    {
        //displays the points remaining, uses formatted string and rich text bold symbol
        textObject.text = $"Points Remaining: <b> {GameManager.instance.GetUpgradePoints()}";
    }
}