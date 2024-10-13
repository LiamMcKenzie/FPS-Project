using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FPSText : MonoBehaviour
{
    private TMP_Text textObject;
    public float updateInterval = 0.25f;
    public float timer;

    void Start()
    {
        textObject = GetComponent<TMP_Text>();  
    }

    void Update()
    {
        if(timer >= updateInterval)
        {
            UpdateFPSDisplay();
            timer = 0;
        }else
        {
            timer += Time.deltaTime;
        }

        if(Time.time % updateInterval == 0) 
        {
            UpdateFPSDisplay();
        }

        if(StaticSettings.fpsDisplay == false)
        {
            textObject.text = "";
        }
    }

    void UpdateFPSDisplay()
    {
        textObject.text = $"{(int)(1f / Time.deltaTime)} FPS";
    }
}
