using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class UiSwitcher : MonoBehaviour
{
    
    [System.Serializable]
    public struct UiObject
    {
        public GameObject gameObject;
        public GameState state;
        public UiObject(GameObject gameObject, GameState state)
        {
            this.gameObject = gameObject;
            this.state = state;
        }
    }

    public List<UiObject> UiObjects = new List<UiObject>();

    void Update()
    {
        foreach (var uiobject in UiObjects)
        {
            uiobject.gameObject.SetActive(GameManager.instance.gameState == uiobject.state);
        }
    }
}
