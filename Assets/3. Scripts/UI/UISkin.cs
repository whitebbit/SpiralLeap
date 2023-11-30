using _3._Scripts.Architecture.Enums;
using _3._Scripts.Architecture.Scriptable;
using HelixJumper.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace _3._Scripts.UI
{
    public abstract class UISkin
    {
        private readonly RectTransform _buyButton;
        private readonly Image _icon;
        private readonly Image _selectImage;
        private readonly Button _button;
        protected Skin _skin;
        protected bool Locked = true;

        protected UISkin(Component obj, Image icon, Image selectImage, RectTransform buyButton)
        {
            _icon = icon;
            _selectImage = selectImage;
            _buyButton = buyButton;
            _button = obj.GetComponentInChildren<Button>();
        }

        public void Initialize(Skin skin)
        {
            _icon.sprite = skin.Image;
            _skin = skin;
            _button.onClick.AddListener(OnClick);
            _selectImage.gameObject.SetActive(false);
        }

        public void SetCostIcon(Image icon, BuyTypeIcons buyTypeIcons)
        {
            icon.sprite = buyTypeIcons.GetIcon(_skin.BuyType);
        }

        public abstract void SetCostText(TextMeshProUGUI text);

        private void OnClick()
        {
            if (!Locked) Select();
            else Buy();
            AudioManager.instance.PlayOneShot("click");
        }

        public void Select()
        {
            _selectImage.gameObject.SetActive(true);
            Ball.instance.skinHolder.ChangeSkin(_skin);
            Shop.instance.SetCurrentSkin(this);
            YandexGame.savesData.currentSkin = _skin.Name;
            YandexGame.SaveProgress();
        }

        public void Unselect()
        {
            _selectImage.gameObject.SetActive(false);
        }

        public void Unlock()
        {
            _buyButton.gameObject.SetActive(false);
            Locked = false;
        }

        protected abstract void Buy();

        public virtual void OnDestroy() {}
    }
}