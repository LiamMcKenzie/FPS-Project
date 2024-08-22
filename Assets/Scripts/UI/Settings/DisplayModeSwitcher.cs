using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DisplayModeSwitcher : MonoBehaviour
{
    private TMP_Dropdown displayDropdown;

    void Awake()
    {
        displayDropdown = GetComponentInChildren<TMP_Dropdown>();
        PopulateDisplayModes();

        LoadSettings();
    }
    
    public void OnDisplayChanged(int index)
    {
        ApplyDisplayMode(index);
    }

    private void PopulateDisplayModes()
    {
        displayDropdown.ClearOptions();
        displayDropdown.AddOptions(new List<TMP_Dropdown.OptionData> {
            new TMP_Dropdown.OptionData("Fullscreen"),
            new TMP_Dropdown.OptionData("Windowed")
        });
    }

    private void ApplyDisplayMode(int index)
    {
        switch (index)
        {
            case 0: //fullscreen
                Screen.fullScreen = true;
                break;
            case 1: //windowed
                Screen.fullScreen = false;
                break;
            default:
                Debug.LogError("Invalid display mode selected!");
                break;
        }

        SaveSettings();
    }

    private void LoadSettings()
    {
        int displayModeIndex = 0; //defaults to fullscreen
        try
        {
            displayModeIndex = PlayerPrefs.GetInt("DisplayMode", 0);
        }
        catch (System.Exception)
        {
            Debug.LogWarning("Display mode set to invalid index");            
        }

        displayDropdown.value = displayModeIndex;
        ApplyDisplayMode(displayModeIndex);
    }

    private void SaveSettings()
    {
        PlayerPrefs.SetInt("DisplayMode", displayDropdown.value);
        PlayerPrefs.Save();
    }

    private void OnApplicationQuit()
    {
        SaveSettings();
    }


}