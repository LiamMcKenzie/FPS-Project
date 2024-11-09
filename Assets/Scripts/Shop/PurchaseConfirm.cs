using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class PurchaseConfirm : MonoBehaviour
{
    private int creditCost;
    private int creditsToPurchase;
    private int skinIndex;
    private string purchaseString;
    private WeaponType weaponType;
    //public GameObject purchaseOverlay;

    private bool isWeaponSkinPurchase;

    public TMP_Text purchaseText;
    
    public void Purchase()
    {
        if(isWeaponSkinPurchase)
        {
            PurchaseSkinConfirm();
        }else
        {
            PurchaseCreditsConfirm(creditsToPurchase);
        }
    }

    void PurchaseSkinConfirm()
    {
        if (PlayerData.instance.playerCurrency >= creditCost)
        {
            PlayerData.instance.playerCurrency -= creditCost;
            switch (weaponType)
            {
                case WeaponType.Pistol:
                    PlayerData.instance.unlockedPistolSkins[skinIndex] = true;
                    break;

                case WeaponType.Shotgun:
                    PlayerData.instance.unlockedShotgunSkins[skinIndex] = true;
                    break;

                default:
                    break;
            }
            PlayerData.instance.SaveToFile();
            //purchaseOverlay.SetActive(false);
        }
    }

    void PurchaseCreditsConfirm(int creditsToPurchase)
    {
        PlayerData.instance.playerCurrency += creditsToPurchase;
        PlayerData.instance.SaveToFile();
    }

    //called when opening modal, sets values to weapon skin purchase
    public void UpdateSkinPurchaseInfo(string newPurchaseString, int cost, int newSkinIndex, WeaponType newWeaponType)
    {
        isWeaponSkinPurchase = true;
        purchaseString = newPurchaseString;
        creditCost = cost;
        skinIndex = newSkinIndex;
        weaponType = newWeaponType;
        purchaseText.text = purchaseString;
    }

    //called when opening modal, sets values to credit purchase
    public void UpdateCreditPurchaseInfo(string newPurchaseString, int newCreditsToPurchase)
    {
        isWeaponSkinPurchase = false;
        purchaseString = newPurchaseString;
        creditsToPurchase = newCreditsToPurchase;
        purchaseText.text = purchaseString;
    }

    
}
