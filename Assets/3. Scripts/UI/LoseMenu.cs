using System;
using System.Collections.Generic;
using _3._Scripts.AD;
using HelixJumper.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;
using YG;
using Random = UnityEngine.Random;

namespace _3._Scripts.UI
{
    public class LoseMenu : MonoBehaviour
    {
        [SerializeField] private List<TextMeshProUGUI> texts;
        [SerializeField] private Button restartButton;
        [SerializeField] private Button continueButton;

        private bool _secondChanceUsed;

        private RewardAdObject _rewardAdObject;

        private void Start()
        {
            _rewardAdObject = new RewardAdObject();
            restartButton.onClick.AddListener(() =>
            {
                SceneManager.LoadScene(1);
                AudioManager.instance.PlayOneShot("click");
            });
            continueButton.onClick.AddListener(() =>
            {
                YandexGame.RewVideoShow(_rewardAdObject.id);
            });
        }

        private void OnEnable()
        {
            YandexGame.RewardVideoEvent += ContinueReward;

            var rand = Random.Range(0, texts.Count);
            foreach (var text in texts)
            {
                text.gameObject.SetActive(false);
            }

            texts[rand].gameObject.SetActive(true);
            continueButton.transform.parent.gameObject.SetActive(!_secondChanceUsed);
        }

        private void OnDisable()
        {
            YandexGame.RewardVideoEvent -= ContinueReward;
        }

        private void ContinueReward(int id)
        {
            if (_rewardAdObject.id != id) return;
            _secondChanceUsed = true;
            Ball.instance.Continue();
            GameManager.instance.ChangePanel(GameManager.instance.PlayPanel);
            AudioManager.instance.PlayOneShot("reward");
        }
    }
}