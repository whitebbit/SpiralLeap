using System;
using System.Threading.Tasks;
using _3._Scripts.Architecture.Enums;
using _3._Scripts.Game;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;
using YG;

namespace _3._Scripts.UI
{
    public class UIPremium : MonoBehaviour
    {
        [SerializeField] private string id;
        [SerializeField] private Sprite secretSkinUnlockedIcon;
        [SerializeField] private Button buyButton;
        [SerializeField] private Button closeButton;

        private void Start()
        {
            buyButton.onClick.AddListener(() =>
            {
                AudioManager.instance.PlayOneShot(AudioManager.instance.Config.UIClick);
                YandexGame.BuyPayments(id);
            });
            closeButton.onClick.AddListener(() =>
            {
                AudioManager.instance.PlayOneShot(AudioManager.instance.Config.UIClick);
                GameManager.instance.ChangePanel(GameManager.instance.MenuPanel);
            });
        }

        private void OnEnable()
        {
            YandexGame.PurchaseSuccessEvent += SuccessPurchased;
        }

        private void OnDisable()
        {
            YandexGame.PurchaseSuccessEvent -= SuccessPurchased;
        }

        private async void SuccessPurchased(string id)
        {
            if (id != this.id) return;

            YandexGame.savesData.premium = true;
            MoneyWidget.money += 5000;
            var skin = Configuration.instance.skinsHolder.GetSkinByType(BuyType.Premium);
            skin.ChangeIcon(secretSkinUnlockedIcon);
            YandexGame.savesData.unlockedSkins.Add(skin.Name);
            AudioManager.instance.PlayOneShot(AudioManager.instance.Config.OnReward);

            await Task.Delay(1000);
            SceneManager.LoadScene("Main");
        }
    }
}