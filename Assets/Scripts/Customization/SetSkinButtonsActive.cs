using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetSkinButtonsActive : MonoBehaviour
{
    private Button[] buttons;
    public WeaponType weaponType;
    public void Start()
    {
        buttons = GetComponentsInChildren<Button>();
    }

    void Update()
    {
        switch (weaponType)
        {
            case WeaponType.Pistol:
                for (int i = 0; i < PlayerData.instance.unlockedPistolSkins.Count; i++)
                {
                    buttons[i].interactable = PlayerData.instance.unlockedPistolSkins[i];
                }
                break;

            case WeaponType.Shotgun:
                for (int i = 0; i < PlayerData.instance.unlockedShotgunSkins.Count; i++)
                {
                    buttons[i].interactable = PlayerData.instance.unlockedShotgunSkins[i];
                }
                break;
            default:
                break;
        }
    }
}
