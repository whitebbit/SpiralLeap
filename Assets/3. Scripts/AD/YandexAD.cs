﻿using Plugins.Audio.Core;
using YG;

namespace _3._Scripts.AD
{
    public static class YandexAD
    {
        public static void ShowInterstitial()
        {
            if(YandexGame.savesData.premium) return;
            
            AudioPauseHandler.Instance.PauseAudio();
            YandexGame.FullscreenShow();
        }
    }
}