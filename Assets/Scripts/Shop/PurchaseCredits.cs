using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PurchaseCredits : MonoBehaviour
{
    public int creditsToPurchase;

    //public Button purchaseButton;

    public void UpdatePurchasedCredits(int credits)
    {
        creditsToPurchase = credits;
    }

    public void AddCredits()
    {
        PlayerData.instance.playerCurrency += creditsToPurchase;
        PlayerData.instance.SaveToFile();
    }
}
