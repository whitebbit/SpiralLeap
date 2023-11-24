using System;
using TMPro;
using UnityEngine;
using YG;

namespace _3._Scripts.UI
{
    public class UILevelWidget: MonoBehaviour
    {

        private LangYGAdditionalText _langYgAdditionalText;
        private TextMeshProUGUI _textMesh;
        private void Awake()
        {
            _langYgAdditionalText = GetComponent<LangYGAdditionalText>();
            if (_langYgAdditionalText == null)
                _textMesh = GetComponent<TextMeshProUGUI>();
        }

        private void Start()
        {
            GetLoad();
        }
        
        private void GetLoad()
        {
            try
            {
                SetLevel();
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                throw;
            }
        }

        private void SetLevel() 
        {
            var level = YandexGame.savesData.currentLevel.ToString();
            if (_langYgAdditionalText == null)
                _textMesh.text = $"{level}";
            else
                _langYgAdditionalText.additionalText = $" {level}";
        }
    }
}