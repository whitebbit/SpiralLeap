using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace _3._Scripts.UI
{
    public class UISkinPremium : UISkin
    {
        public UISkinPremium(Component obj, Image icon, Image selectImage, RectTransform buyButton) : base(obj, icon,
            selectImage, buyButton)
        {
        }

        public override void SetCostText(TextMeshProUGUI text)
        {
            text.text = "?????";
        }

        protected override void Buy()
        {
        }
    }
}