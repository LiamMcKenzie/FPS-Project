using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// This script updates the sensitivity slider and input field
/// It will be used to change the mouse sensitivity
/// </summary>
/// <remarks>
/// 
/// </remarks>
public class SensitivitySlider : MonoBehaviour
{
    
    private Slider sensitivitySlider;
    private TMP_InputField sensitivityInputField;
    public float minValue = 0.1f;
    public float maxValue = 5f;

    void Start()
    {
        sensitivityInputField = GetComponentInChildren<TMP_InputField>();
        sensitivitySlider = GetComponentInChildren<Slider>();

        LoadSettings();
    }

    public void OnSliderValueChanged()
    {
        SaveSettings();
        sensitivityInputField.text = sensitivitySlider.value.ToString("F2");
    }

    public void OnInputFieldEndEdit()
    {
        //attempts to convert the string to a float, if it fails it will not update the slider
        float value;
        if (float.TryParse(sensitivityInputField.text, out value)) //converts the string to a float
        {
            sensitivitySlider.value = value;
            SaveSettings();
            //set text to minimum or maximum value if out of range.
            if(value < minValue)
            {
                sensitivitySlider.value = minValue;
                sensitivityInputField.text = minValue.ToString("F2");
            }
            else if(value > maxValue)
            {
                sensitivitySlider.value = maxValue;
                sensitivityInputField.text = maxValue.ToString("F2");
            }
        }else
        {
            //if the string cannot be converted to a float, set the input field to the slider value
            sensitivityInputField.text = sensitivitySlider.value.ToString("F2");
        }

        
    }

    private void LoadSettings()
    {
        sensitivitySlider.minValue = minValue;
        sensitivitySlider.maxValue = maxValue;

        Debug.Log(SaveManager.instance);

        sensitivitySlider.value = SaveManager.instance.mouseSensitivity;
        sensitivityInputField.text = sensitivitySlider.value.ToString("F2");
    }

    private void SaveSettings()
    {
        SaveManager.instance.mouseSensitivity = sensitivitySlider.value;
        SaveManager.instance.SaveToFile();
    }

    private void OnApplicationQuit()
    {
        SaveSettings();
    }
}
