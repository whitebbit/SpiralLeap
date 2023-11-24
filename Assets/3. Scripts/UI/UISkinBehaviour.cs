using System;
using System.Linq;
using _3._Scripts.AD;
using _3._Scripts.Architecture.Enums;
using _3._Scripts.Architecture.Extensions;
using _3._Scripts.Architecture.Scriptable;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using HelixJumper.Scripts;
using YG;

namespace _3._Scripts.UI
{
    public class UISkinBehaviour : MonoBehaviour
    {
        [Header("Main UI")] [SerializeField] protected Image icon;
        [Space] [SerializeField] protected Image costIcon;
        [SerializeField] protected TextMeshProUGUI costText;

        [Header("Additional UI")] [SerializeField]
        protected RectTransform buyButton;
        [SerializeField] protected Image selectImage;


        private UISkin _uiSkin;
        public void Initialize(Skin skin)
        {
            _uiSkin = skin.BuyType switch
            {
                BuyType.Coins => new UISkinCoins(this, icon, selectImage, buyButton),
                BuyType.Ad => new UISkinAd(this, icon, selectImage, buyButton),
                BuyType.None =>  new UISkinDefault(this, icon, selectImage, buyButton),
                BuyType.Premium =>  new UISkinPremium(this, icon, selectImage, buyButton),
                _ => throw new ArgumentOutOfRangeException()
            };
            
            _uiSkin.Initialize(skin);
            _uiSkin.SetCostIcon(costIcon, Configuration.instance.buyTypeIcons);
            _uiSkin.SetCostText(costText);

            if (YandexGame.savesData.unlockedSkins.FirstOrDefault(s => s == skin.Name) != null)
                _uiSkin.Unlock();
            
            if(YandexGame.savesData.currentSkin == skin.Name)
                _uiSkin.Select();
            
        }
        private void OnDestroy()
        {
            _uiSkin.OnDestroy();
        }
    }
}