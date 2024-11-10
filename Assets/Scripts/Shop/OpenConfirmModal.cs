using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenConfirmModal : MonoBehaviour
{
    public GameObject confirmModal;
    public int creditCost;
    public int skinIndex;
    public WeaponType weaponType;
    public string purchaseString;

    public bool isWeaponSkinPurchase;

    public GameObject purchaseOverlay;
    public Button purchaseButton;   

    //
    public void Update()
    {
        if(isWeaponSkinPurchase) //if the script is attached to a weapon skin purchase button, check if the skin is already unlocked.
        {
            switch (weaponType)
            {
                case WeaponType.Pistol:
                    if(PlayerData.instance.unlockedPistolSkins[skinIndex])
                    {
                        purchaseOverlay.SetActive(true);
                        purchaseButton.enabled = false;
                    }
                    break;

                case WeaponType.Shotgun:
                    if(PlayerData.instance.unlockedShotgunSkins[skinIndex])
                    {
                        purchaseOverlay.SetActive(true);
                        purchaseButton.enabled = false;
                    }
                    break;

                default:
                    break;
            }
        }
    }

    public void OpenModal()
    {
        if(isWeaponSkinPurchase)
        {   
            if(creditCost <= PlayerData.instance.playerCurrency)
            {
                confirmModal.SetActive(true);
                confirmModal.GetComponent<PurchaseConfirm>().UpdateSkinPurchaseInfo(purchaseString, creditCost, skinIndex, weaponType);
            }
        }else
        {
            confirmModal.SetActive(true);
            confirmModal.GetComponent<PurchaseConfirm>().UpdateCreditPurchaseInfo(purchaseString, creditCost);
        }
        
    }
}
