using _3._Scripts.Architecture.Enums;
using _3._Scripts.Architecture.Extensions;
using _3._Scripts.Architecture.Scriptable;
using _3._Scripts.Game;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace _3._Scripts.UI
{
    public class UISkinCoins : UISkin
    {
        public UISkinCoins(Component obj, Image icon, Image selectImage, RectTransform buyButton) : base(obj, icon,
            selectImage, buyButton)
        {
        }


        public override void SetCostText(TextMeshProUGUI text)
        {
            text.text = "1000";
            text.Resize();
        }

        protected override void Buy()
        {
            if (MoneyWidget.money < 1000) return;
            
            MoneyWidget.money -= 1000;
            YandexGame.savesData.unlockedSkins.Add(_skin.Name);
            YandexGame.SaveProgress();
            Unlock();
        }
    }
}