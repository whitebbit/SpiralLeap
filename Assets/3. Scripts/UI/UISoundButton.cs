using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using YG;

namespace _3._Scripts.UI
{
    [RequireComponent(typeof(Button))]
    public class UISoundButton: MonoBehaviour
    {
        [SerializeField] private AudioMixer audioMixer;
        [Space] [SerializeField] private Image icon;
        [SerializeField] private Sprite on;
        [SerializeField] private Sprite off;
        private Button _button;
        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void Start()
        {
            GetLoad();
            _button.onClick.AddListener(ChangeVolume);
        }
        
        private void GetLoad()
        {
            try
            {
                SetIcon();
                SetVolume();
                AudioManager.instance.MusicState(true);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                throw;
            }
        }
        private void SetIcon()
        {
            var active = YandexGame.savesData.soundActive;
            var sprite = active ? on : off;
            icon.sprite = sprite;
        }
        private void SetVolume()
        {
            var active = YandexGame.savesData.soundActive;
            var volume = active ? 0 : -80;
            audioMixer.SetFloat("SoundsVolume", volume);
        }
        private void ChangeVolume()
        {
            AudioManager.instance.PlayOneShot(AudioManager.instance.Config.UIClick);
            YandexGame.savesData.soundActive = !YandexGame.savesData.soundActive;
            YandexGame.SaveProgress();
            SetIcon();
            SetVolume();
        }
    }
}