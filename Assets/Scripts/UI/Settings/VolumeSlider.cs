using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// This script updates the volume slider and input field
/// It will be used to change the audio volume
/// </summary>
/// <remarks>
/// 
/// </remarks>
public class VolumeSlider : MonoBehaviour
{
    
    private Slider volumeSlider;
    private TMP_InputField volumeInputField;
    public float minValue = 0.1f;
    public float maxValue = 5f;

    void Start()
    {
        volumeInputField = GetComponentInChildren<TMP_InputField>();
        volumeSlider = GetComponentInChildren<Slider>();

        LoadSettings();
    }

    public void OnSliderValueChanged()
    {
        SaveSettings();
        volumeInputField.text = volumeSlider.value.ToString("F2");
    }

    public void OnInputFieldEndEdit()
    {
        //attempts to convert the string to a float, if it fails it will not update the slider
        float value;
        if (float.TryParse(volumeInputField.text, out value)) //converts the string to a float
        {
            volumeSlider.value = value;
            SaveSettings();
            //set text to minimum or maximum value if out of range.
            if(value < minValue)
            {
                volumeSlider.value = minValue;
                volumeInputField.text = minValue.ToString("F2");
            }
            else if(value > maxValue)
            {
                volumeSlider.value = maxValue;
                volumeInputField.text = maxValue.ToString("F2");
            }
        }else
        {
            //if the string cannot be converted to a float, set the input field to the slider value
            volumeInputField.text = volumeSlider.value.ToString("F2");
        }

        
    }

    private void LoadSettings()
    {
        volumeSlider.minValue = minValue;
        volumeSlider.maxValue = maxValue;

        volumeSlider.value = SaveManager.instance.volume;
        volumeInputField.text = volumeSlider.value.ToString("F2");
    }

    private void SaveSettings()
    {
        SaveManager.instance.volume = volumeSlider.value;
        SaveManager.instance.SaveToFile();
    }

    private void OnApplicationQuit()
    {
        SaveSettings();
    }
}
