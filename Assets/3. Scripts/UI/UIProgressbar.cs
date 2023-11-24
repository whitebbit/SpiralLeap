using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _3._Scripts.UI
{
    public class UIProgressbar: MonoBehaviour
    {
        [SerializeField] private Image fill;

        private void Awake()
        {
            fill.fillAmount = 0;
        }

        public void UpdateValue(float current, float max)
        {
            var endValue = current / max;
            fill.DOFillAmount(endValue,0.25f);
        }
    }
}