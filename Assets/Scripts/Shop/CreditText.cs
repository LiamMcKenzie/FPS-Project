using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CreditText : MonoBehaviour
{
    private TMP_Text textObject;
    void Start()
    {
        textObject = GetComponent<TMP_Text>();  
    }

    void Update()
    {
        textObject.text = PlayerData.instance.playerCurrency.ToString();
    }
}
