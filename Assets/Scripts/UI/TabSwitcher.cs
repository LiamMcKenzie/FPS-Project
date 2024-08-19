using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script is used to switch between UI tabs.
/// Each tab has a gameobject and button pair.
/// </summary>

public class TabSwitcher : MonoBehaviour
{
    [System.Serializable]
    public struct UITab
    {
        public GameObject uiSection;
        public Button tabButton;
        public UITab(GameObject uiSection, Button tabButton)
        {
            this.uiSection = uiSection;
            this.tabButton = tabButton;
        }
    }

    [SerializeField] public List<UITab> tabs = new List<UITab>();

    void Start()
    {
        //loops through all tabs and adds click events to each button
        foreach (var tab in tabs) 
        {
            tab.tabButton.onClick.AddListener(() => ShowTab(tab));
        }
    }

    void ShowTab(UITab activeTab)
    {
        //sets all sections to inactive and makes all buttons interactable
        foreach (var tab in tabs)
        {
            tab.uiSection.SetActive(false);
            tab.tabButton.interactable = true;
        }

        //makes the selected tab section visible and makes the tab button unclickable
        activeTab.uiSection.SetActive(true);
        activeTab.tabButton.interactable = false;

    }
}
