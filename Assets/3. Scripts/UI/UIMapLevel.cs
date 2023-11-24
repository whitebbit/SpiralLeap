using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using YG;
using Random = UnityEngine.Random;

namespace _3._Scripts.UI
{
    public class UIMapLevel : MonoBehaviour
    {
        [SerializeField] private LangYGAdditionalText levelText;
        [SerializeField] private Image star;
        [SerializeField] private Image starBackground;
        [SerializeField] private Image mainImage;
        public RectTransform rectTransform { get; private set; }
        private Color _startColor;

        private void Awake()
        {
            _startColor = mainImage.color;
            if (rectTransform == null) rectTransform = GetComponent<RectTransform>();
        }

        public void SetLevel(int level)
        {
            levelText.additionalText = $" {level}";
        }
        public Tween LevelComplete()
        {
            var color = star.color;
            color.a = 0;

            star.color = color;
            star.rectTransform.localScale = Vector2.zero;

            star.DOFade(1, 1.5f);
            return star.rectTransform.DOScale(1, .75f).SetEase(Ease.OutBounce);
        }

        public Tween Unlock()
        {
            starBackground.DOColor(Color.white, 1);
            return mainImage.DOColor(Color.white, 1);
        }
        
        public Vector2 SetRandomPosition(float minX, float maxX, float y)
        {
            if (rectTransform == null) return Vector3.zero;
            rectTransform.anchoredPosition = new Vector2(Random.Range(minX, maxX), y);
            return rectTransform.anchoredPosition;
        }

        public Vector2 SetPosition(Vector2 position)
        {
            if (rectTransform == null) return Vector3.zero;
            rectTransform.anchoredPosition = position;
            return rectTransform.anchoredPosition;
        }

        public void ResetObject()
        {
            starBackground.color = _startColor;
            mainImage.color = _startColor;
        }
    }
}