using System;
using _3._Scripts.AD;
using _3._Scripts.Architecture.Enums;
using _3._Scripts.Architecture.Extensions;
using _3._Scripts.Architecture.Scriptable;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace _3._Scripts.UI
{
    public class UISkinAd : UISkin
    {
        private readonly RewardAdObject _rewardAdObject;

        private void OnEnable() => YandexGame.RewardVideoEvent += OnBuy;
        private void OnDisable() => YandexGame.RewardVideoEvent -= OnBuy;

        public UISkinAd(Component obj, Image icon, Image selectImage, RectTransform buyButton) : base(obj, icon,
            selectImage, buyButton)
        {
            YandexGame.RewardVideoEvent += OnBuy;
            _rewardAdObject = new RewardAdObject();
        }
        

        public override void SetCostText(TextMeshProUGUI text)
        {
            text.text = "AD";
            text.Resize();
        }

        protected override void Buy()
        {
            YandexGame.RewVideoShow(_rewardAdObject.id);
        }

        private void OnBuy(int id)
        {
            if (id != _rewardAdObject.id) return;
            YandexGame.savesData.unlockedSkins.Add(_skin.Name);
            AudioManager.instance.PlayOneShot("reward");
            YandexGame.SaveProgress();
            Unlock();
        }

        public virtual void OnDestroy()
        {
            YandexGame.RewardVideoEvent -= OnBuy;
        }
    }
}