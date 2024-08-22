using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FrameRateSwitcher : MonoBehaviour
{
    private TMP_Dropdown frameRateDropdown;
    
    void Awake()
    {
        frameRateDropdown = GetComponentInChildren<TMP_Dropdown>();
        PopulateFrameRates();

        LoadSettings();
    }

    public void OnFrameRateChanged(int index)
    {
        ApplyFrameRate(index);
    }

    private void PopulateFrameRates()
    {
        frameRateDropdown.ClearOptions();
        frameRateDropdown.AddOptions(new List<string> {
            "60 FPS",
            "120 FPS",
            "144 FPS",
            "240 FPS",
            "Unlimited"
        });
    }

    private void ApplyFrameRate(int index)
    {
        string selectedOption = frameRateDropdown.options[index].text;

        switch(selectedOption)
        {
            case "60 FPS":
                Application.targetFrameRate = 60;
                break;
            case "120 FPS":
                Application.targetFrameRate = 120;
                break;
            case "144 FPS":
                Application.targetFrameRate = 144;
                break;
            case "240 FPS":
                Application.targetFrameRate = 240;
                break;
            case "Unlimited":
                Application.targetFrameRate = -1; //uncapped frame rate
                break;
            default:
                Debug.LogError("Invalid frame rate selected!");
                break;
        }

        SaveSettings();
    }

    private void LoadSettings()
    {
        int frameRateIndex = 0; //defaults to 60
        try
        {
            frameRateIndex = PlayerPrefs.GetInt("FrameRate", 0);
        }
        catch (System.Exception)
        {
            Debug.LogWarning("Frame rate set to invalid index");            
        }

        frameRateDropdown.value = frameRateIndex;
        ApplyFrameRate(frameRateIndex);
    }

    private void SaveSettings()
    {
        PlayerPrefs.SetInt("FrameRate", frameRateDropdown.value);
        PlayerPrefs.Save();
    }

    private void OnApplicationQuit()
    {
        SaveSettings();
    }
}
