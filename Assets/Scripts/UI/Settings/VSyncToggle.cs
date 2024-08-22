using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class VSyncToggle : MonoBehaviour
{
    private Toggle vsyncToggle;

    void Awake()
    {
        vsyncToggle = GetComponentInChildren<Toggle>();
        LoadSettings();
    }

    public void OnVSyncChanged(bool value)
    {
        ApplyVSync(value);
    }

    private void ApplyVSync(bool value)
    {
        if(value)
        {
            QualitySettings.vSyncCount = 1;
        }
        else
        {
            QualitySettings.vSyncCount = 0;
        }
    }

    private void LoadSettings()
    {
        bool vsyncEnabled = true;
        try
        {
            vsyncEnabled = PlayerPrefs.GetInt("Vsync", 0) == 1; //playerprefs doesn't support booleans, it uses 0 and 1 instead. returns true if playerprefs value is 1.
        }
        catch
        {
            Debug.LogWarning("VSync set to invalid value, setting to default (true)");
        }
        vsyncToggle.isOn = vsyncEnabled;
        ApplyVSync(vsyncEnabled);
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("Vsync", vsyncToggle.isOn ? 1 : 0); //converts bool to 0 or 1. true == 1
        PlayerPrefs.Save();
    }
}
