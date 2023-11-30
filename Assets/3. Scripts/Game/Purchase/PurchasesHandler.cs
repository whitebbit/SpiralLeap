using System;
using System.Collections.Generic;
using UnityEngine;
using YG;

namespace _3._Scripts.Game.Purchase
{
    public class PurchasesHandler: MonoBehaviour
    {
        [SerializeField] private List<Purchase> purchases;
        
        private void OnEnable()
        {
            YandexGame.PurchaseSuccessEvent += SuccessPurchased;
            YandexGame.PurchaseFailedEvent -= FailedPurchased;
        }
        
        private void OnDisable()
        {
            YandexGame.PurchaseSuccessEvent -= SuccessPurchased;
            YandexGame.PurchaseFailedEvent -= FailedPurchased;
        }
        
        private void SuccessPurchased(string obj)
        {
            foreach (var purchase in purchases)
            {
                purchase.SuccessPurchased(obj);
            }
        }
        
        private void FailedPurchased(string obj)
        {
            foreach (var purchase in purchases)
            {
                purchase.FailedPurchased(obj);
            }
        }
        
    }
}