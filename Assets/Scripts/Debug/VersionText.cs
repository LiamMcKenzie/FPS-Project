using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VersionText : MonoBehaviour
{
    private TMP_Text textObject;
    void Start()
    {
        textObject = GetComponent<TMP_Text>();  
        textObject.text = ($"Build: v{Application.version}");
    }
}
