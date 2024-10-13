using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetButtonColour : MonoBehaviour
{
    private Image image;
    public Color green;
    public Color yellow;

    void Start()
    {
        image = GetComponent<Image>();
    }

    void Update()
    {
        if(GameManager.instance.GetUpgradePoints() == 0)
        {
            image.color = green;
        } 
        else{

            image.color = yellow;
        }
    }
}