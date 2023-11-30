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
        [SerializeField] private Button buyButton;
        [SerializeField] private Button closeButton;

        private void Start()
        {
            buyButton.onClick.AddListener(() =>
            {
                AudioManager.instance.PlayOneShot("click");
                YandexGame.BuyPayments(id);
            });
            closeButton.onClick.AddListener(() =>
            {
                AudioManager.instance.PlayOneShot("click");
                GameManager.instance.ChangePanel(GameManager.instance.MenuPanel);
            });
        }
    }
}