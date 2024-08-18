using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// This script accesses the EventSystem to manually set the selected object to the hovered button.
/// </summary>
/// 
/// <remarks>
/// USE: Attach this script to a button, make sure the button has an EventTrigger component. 
/// The EventTrigger component should have a pointerEnter and pointerExit event that calls the corresponding functions.
/// </remarks>

[RequireComponent(typeof(EventTrigger))] //unnessary but should save errors.
public class ButtonSelected : MonoBehaviour
{
    
    public void OnHoverEnter() //make sure this is called by the pointerEnter trigger
    {
        EventSystem.current.SetSelectedGameObject(gameObject);
    }

    public void OnHoverExit()  //make sure this is called by the pointerExit trigger
    {
        EventSystem.current.SetSelectedGameObject(null);
    }
}
