using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;
using Random = UnityEngine.Random;

namespace _3._Scripts.UI
{
    public class ProgressMenu : MonoBehaviour
    {
        [SerializeField] private UIMapLevel currentLevel;
        [SerializeField] private UIMapLevel nextLevel;
        [SerializeField] private UIMapLevel newLevel;

        [Space] [SerializeField] private Image line;
        [Space] [SerializeField] private RectTransform map;
        [Space] [SerializeField] private RectTransform mapContainer;
        [Space] [SerializeField] private Button continueButton;

        private Vector3 _mapStartPosition;
        private bool _animateLine;

        private static Vector2 _currentLevelPosition;
        private static Vector2 _nextLevelPosition;

        private void Awake()
        {
            _mapStartPosition = map.anchoredPosition;
            continueButton.onClick.AddListener(Continue);
        }

        public void ShowProgress()
        {
            AnimateMap();
            SetLevelsPositions();
            SetLineColor();
            SetLinePosition();
            SetLevels();

            nextLevel.ResetObject();
            newLevel.ResetObject();
            
            mapContainer.anchoredPosition = Vector2.zero;
            currentLevel.LevelComplete()
                .OnComplete(() => AnimateLine().OnComplete(() =>
                {
                    nextLevel.Unlock();
                    MoveMap();
                }));
        }

        private void Continue()
        {
            GameManager.instance.NextLevel();
        }

        private void SetLevelsPositions()
        {
            if (_currentLevelPosition == Vector2.zero)
            {
                var nextLevelPos = nextLevel.SetRandomPosition(-200, 200, 400f);
                var newLevelPos = newLevel.SetRandomPosition(-200, 200, 1200f);
                
                currentLevel.SetRandomPosition(-200, 200, -400f);
                _currentLevelPosition = new Vector2(nextLevelPos.x, -400f);
                _nextLevelPosition = new Vector2(newLevelPos.x, 400);
            }
            else
            {
                currentLevel.SetPosition(_currentLevelPosition);
                var nextLevelPos = nextLevel.SetPosition(_nextLevelPosition);
                var newLevelPos = newLevel.SetRandomPosition(-200, 200, 1200f);

                _currentLevelPosition = new Vector2(nextLevelPos.x, -400f);
                _nextLevelPosition = new Vector2(newLevelPos.x, 400f);
            }
        }

        private void SetLineColor()
        {
            var color = line.color;
            color.a = 0;
            line.color = color;
        }

        private void SetLinePosition()
        {
            if (currentLevel.rectTransform == null) return;

            var direction = nextLevel.rectTransform.anchoredPosition - currentLevel.rectTransform.anchoredPosition;
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            var rotation = line.rectTransform.eulerAngles;
            var position = line.rectTransform.anchoredPosition;

            rotation.z = angle;
            position.x = (nextLevel.rectTransform.anchoredPosition.x + currentLevel.rectTransform.anchoredPosition.x) *
                         0.5f;

            line.rectTransform.eulerAngles = rotation;
            line.rectTransform.anchoredPosition = position;
        }

        private void SetLevels()
        {
            try
            {
                var currentLevelNumber = YandexGame.savesData.currentLevel;
                currentLevel.SetLevel(currentLevelNumber);
                nextLevel.SetLevel(currentLevelNumber + 1);
                newLevel.SetLevel(currentLevelNumber + 2);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                throw;
            }
        }

        private Tween AnimateLine()
        {
            return line.DOFade(1, 1);
        }

        private void AnimateMap()
        {
            map.anchoredPosition = _mapStartPosition;
            map.DOAnchorPos(_mapStartPosition - Vector3.right * 750, 25).SetEase(Ease.Linear);
        }

        private void MoveMap()
        {
            var endValue = new Vector2(0, -840);

            mapContainer.anchoredPosition = Vector2.zero;
            mapContainer.DOAnchorPos(endValue, 2f);
        }
    }
}