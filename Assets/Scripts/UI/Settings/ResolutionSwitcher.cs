using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// This script updates the resolutions dropdown and changes the window resolution
/// </summary>
/// 
/// <remarks>
/// USE: attach this script to a parent object, with the dropdown as a child.
/// On the dropdown child, set its OnValueChanged function to Dynamic OnResolutionChanged (at the top)
/// </remarks>

public class ResolutionSwitcher : MonoBehaviour
{
    private TMP_Dropdown resolutionDropdown;

    private List<Resolution> resolutions = new List<Resolution>();

    void Awake()
    {
        resolutionDropdown = GetComponentInChildren<TMP_Dropdown>();
        PopulateResolutions();

        LoadSettings();
    }

    /// <summary>
    /// Public method to handle the resolution dropdown value changed event
    /// </summary>
    /// <param name="index">Selected resolution</param>
    public void OnResolutionChanged(int index)
    {
        ApplyResolution(index);
    }

    /// <summary>
    /// Populates the resolution dropdown
    /// </summary>
    private void PopulateResolutions()
    {
        Resolution[] originalResolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        //This uses a hashset as it is the most efficient way to check for duplicates
        //If not used then there will be duplicate resolutions for monitors that can do the same res but with different HZ
        HashSet<string> uniqueResolutions = new HashSet<string>();

        foreach (var res in originalResolutions)
        {
            string resolutionString = res.width + "x" + res.height;
            if (!uniqueResolutions.Contains(resolutionString))
            {
                resolutionDropdown.options.Add(new TMP_Dropdown.OptionData(resolutionString));
                uniqueResolutions.Add(resolutionString);
                resolutions.Add(res);
            }
        }
    }

    /// <summary>
    /// Applies the selected resolution
    /// </summary>
    /// <param name="index">Resolution index</param>
    private void ApplyResolution(int index)
    {
        Resolution selectedResolution = resolutions[index];
        Screen.SetResolution(selectedResolution.width, selectedResolution.height, Screen.fullScreen);
    }

    private void LoadSettings()
    {
        int resolutionIndex = resolutionDropdown.options.Count - 1; //Default is the top resolution the monitor supports in case the PlayerPrefs value is invalid
        try
        {
            resolutionIndex = PlayerPrefs.GetInt("Resolution", resolutionDropdown.options.Count - 1); //Default is the top resolution the monitor supports
        }
        catch
        {
            Debug.LogWarning("Resolution set to invalid index, setting to default.");
        }
        resolutionDropdown.value = resolutionIndex;
        ApplyResolution(resolutionIndex);    
    }

    public void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("Resolution", resolutionDropdown.value);
        PlayerPrefs.Save();
    }
}
