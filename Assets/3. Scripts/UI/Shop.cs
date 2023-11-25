using System;
using _3._Scripts.Architecture;
using _3._Scripts.Architecture.Scriptable;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using YG;

namespace _3._Scripts.UI
{
    public class Shop : Singleton<Shop>
    {
        [Space] [SerializeField] private Button closeButton;
        [SerializeField] private RectTransform container;
        [SerializeField] private UISkinBehaviour skinPrefab;

        private UISkin _currentSkin;
        private bool _initialized;

        private void Start()
        {
            closeButton.onClick.AddListener(Close);
            GetLoad();
        }

        private void InitializeSkins()
        {
            if(_initialized) return;
            
            var grid = container.GetComponent<GridLayoutGroup>();
            var skins = Configuration.instance.skinsHolder;
            container.offsetMin = new Vector2(0, -(skins.Skins.Count / 3 * grid.cellSize.x));
            foreach (var skin in skins.Skins)
            {
                var obj = Instantiate(skinPrefab, container);
                obj.Initialize(skin);
            }

            _initialized = true;
        }

        public void SetCurrentSkin(UISkin skinBehaviour)
        {
            if (_currentSkin == skinBehaviour) return;

            _currentSkin?.Unselect();
            _currentSkin = skinBehaviour;
        }

        private void Close()
        {
            AudioManager.instance.PlayOneShot(AudioManager.instance.Config.UIClick);
            GameManager.instance.ChangePanel(GameManager.instance.MenuPanel);
        }

        private void GetLoad()
        {
            try
            {
                InitializeSkins();
            }
            catch (Exception e)
            {
                _initialized = false;
                Debug.LogError(e);
                throw;
            }
        }
    }
}