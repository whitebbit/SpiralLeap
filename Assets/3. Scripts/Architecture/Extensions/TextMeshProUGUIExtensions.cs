using TMPro;
using UnityEngine;

namespace _3._Scripts.Architecture.Extensions
{
    public static class TextMeshProUGUIExtensions
    {
        public static void Resize(this TextMeshProUGUI text)
        {
            var textSize = text.GetPreferredValues();
            
            var width = textSize.x;
            var height = textSize.y;

            text.rectTransform.sizeDelta = new Vector2(width,height);
        }
    }
}