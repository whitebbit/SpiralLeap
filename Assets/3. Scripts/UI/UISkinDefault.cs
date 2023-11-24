using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _3._Scripts.UI
{
    public class UISkinDefault: UISkin
    {
        public UISkinDefault(Component obj, Image icon, Image selectImage, RectTransform buyButton) : base(obj, icon, selectImage, buyButton)
        {
            Unlock();
            Locked = false;
        }

        public override void SetCostText(TextMeshProUGUI text)
        {
            
        }

        protected override void Buy()
        {
            
        }
    }
}