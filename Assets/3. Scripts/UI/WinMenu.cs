using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using _3._Scripts.AD;
using _3._Scripts.Architecture.Extensions;
using _3._Scripts.Game;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace _3._Scripts.UI
{
    public class WinMenu : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI reward;
        [SerializeField] private Button adRewardButton;
        [Space] [SerializeField] private ProgressMenu progressMenu;
        [SerializeField] private Button continueButton;

        [Header("Bonus Game")] [SerializeField]
        private RectTransform indicator;

        [SerializeField] private LangYGAdditionalText multiplierText;
        [SerializeField] private Transform[] pathPoints;

        private RewardAdObject _rewardAdObject;
        private bool _bonusUsed;
        private bool _progressOpened;
        private UIBonusMultiplier _currentMultiplier;
        private readonly List<Tween> _bonusGameTweens = new List<Tween>();

        private void Awake()
        {
            _rewardAdObject = new RewardAdObject();
            continueButton.onClick.AddListener(()=>
            {
                AudioManager.instance.PlayOneShot("click");
                StartCoroutine(OpenProgress());
            });
            adRewardButton.onClick.AddListener(GetBonus);
            reward.text = $"{ScoreManager.instance.levelGoal}<sprite=0>";

            MoveIndicator();
        }

        private void GetBonus()
        {
            if (_bonusUsed) return;
            YandexGame.RewVideoShow(_rewardAdObject.id);
            foreach (var tween in _bonusGameTweens)
            {
                tween.Pause();
                tween.Kill();
            }
        }


        private void OnEnable()
        {
            YandexGame.RewardVideoEvent += StartAdReward;
        }
    
        private void OnDisable()
        {
            YandexGame.RewardVideoEvent -= StartAdReward;
        }

        private IEnumerator OpenProgress()
        {            
            if (_progressOpened) yield break;
            if (!_bonusUsed)
            {
                reward.text = $"{ScoreManager.instance.levelGoal}<sprite=0>";
                foreach (var tween in _bonusGameTweens)
                {
                    tween.Pause();
                    tween.Kill();
                }
            }
            
            MoneyWidget.money += _bonusUsed
                ? ScoreManager.instance.levelGoal * _currentMultiplier.Multiplier
                : ScoreManager.instance.levelGoal;
            
            _progressOpened = true;

            yield return new WaitForSeconds(0.75f);
            
            progressMenu.ShowProgress();
            GameManager.instance.ChangePanel(GameManager.instance.MapPanel);
        }

        private void StartAdReward(int id) => StartCoroutine(AdReward(id));
        private IEnumerator AdReward(int id)
        {
            if (id != _rewardAdObject.id) yield break;
            AudioManager.instance.PlayOneShot("reward");
            _bonusUsed = true;
            reward.text = $"{ScoreManager.instance.levelGoal * _currentMultiplier.Multiplier}<sprite=0>";
            yield return new WaitForSeconds(0.75f);

            StartCoroutine(OpenProgress());
        }

        private void MoveIndicator()
        {
            var path = new Vector3[pathPoints.Length];
            for (var i = 0; i < pathPoints.Length; i++)
            {
                path[i] = pathPoints[i].position;
            }

            indicator.position = path[0];
            var mover = indicator.DOPath(path, 1f, PathType.CatmullRom)
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.Linear)
                .OnUpdate(SetMultiplier);

            var rotator = indicator.DOLocalRotate(new Vector3(0, 0, -45), 1)
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.Linear);

            _bonusGameTweens.Add(mover);
            _bonusGameTweens.Add(rotator);
        }

        private void SetMultiplier()
        {
            var bonusMultiplier = UIRaycast.FindObject<UIBonusMultiplier>(indicator.position);
            if (bonusMultiplier == null) return;
            
            multiplierText.additionalText = $" {bonusMultiplier.Multiplier}X";
            reward.text = $"{ScoreManager.instance.levelGoal * bonusMultiplier.Multiplier}<sprite=0>";
            _currentMultiplier = bonusMultiplier;
        }
    }
}