using System;
using _3._Scripts.Architecture.Extensions;
using TMPro;
using UnityEngine;

namespace _3._Scripts.UI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class AutosizebleText: MonoBehaviour
    {
        private void Start()
        {
            var text = GetComponent<TextMeshProUGUI>();
            text.Resize();
        }
    }
}