using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveCount : MonoBehaviour
{
    private TMP_Text textObject;
    public string preText = "Wave: ";
    void Start()
    {
        textObject = gameObject.GetComponent<TMP_Text>();
    }

    void Update()
    {
        //displays the points remaining, uses formatted string and rich text bold symbol
        textObject.text = $"{preText}{GameManager.instance.GetWaveCountText()}";
    }
}