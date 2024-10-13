using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FPSDisplayToggle : MonoBehaviour
{
    private Toggle fpsToggle;

    void Awake()
    {
        fpsToggle = GetComponentInChildren<Toggle>();
        LoadSettings();
    }

    public void OnVSyncChanged(bool value)
    {
        ApplyVSync(value);
    }

    private void ApplyVSync(bool value)
    {
        StaticSettings.fpsDisplay = value;

        SaveSettings();
    }

    private void LoadSettings()
    {
        bool fpsEnabled = false;
        try
        {
            fpsEnabled = PlayerPrefs.GetInt("Vsync", 0) == 1; //playerprefs doesn't support booleans, it uses 0 and 1 instead. returns true if playerprefs value is 1.
        }
        catch
        {
            Debug.LogWarning("FPS Display set to invalid value, setting to default (false)");
        }
        fpsToggle.isOn = fpsEnabled;
        ApplyVSync(fpsEnabled);
    }

    private void SaveSettings()
    {
        PlayerPrefs.SetInt("FPS Display", fpsToggle.isOn ? 1 : 0); //converts bool to 0 or 1. true == 1
        PlayerPrefs.Save();
    }

    private void OnApplicationQuit()
    {
        SaveSettings();
    }
}
