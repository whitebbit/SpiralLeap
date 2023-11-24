using System;
using _3._Scripts.Architecture.Enums;
using _3._Scripts.Architecture.Scriptable;
using _3._Scripts.Game;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace _3._Scripts.UI
{
    public class UIMoneyWidget: MonoBehaviour
    {
        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI text;

        private void Awake()
        {
            icon.sprite = Configuration.instance.buyTypeIcons.GetIcon(BuyType.Coins);
        }

        private void Start()
        {
            GetLoad();
        }

        private void OnEnable()
        { 
            MoneyWidget.OnChange += Change;
        }
        private void OnDisable()
        {
            MoneyWidget.OnChange += Change;
        }
        
        private void GetLoad()
        {
            try
            {
                text.DOCounter(MoneyWidget.money, MoneyWidget.money, 0f);
            }
            catch (Exception e)
            {
               
                Debug.LogError(e);
                throw;
            }
        }

        private void Change(int fromValue, int endValue)
        {
            text.DOCounter(fromValue, endValue,0.5f);
        }
    }
}