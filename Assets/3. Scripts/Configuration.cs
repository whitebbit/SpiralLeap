﻿using System;
using System.Collections;
using System.Threading.Tasks;
using _3._Scripts.Architecture;
using _3._Scripts.Architecture.Scriptable;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

namespace _3._Scripts
{
    public class Configuration : Singleton<Configuration>
    {
        [field: SerializeField] public SkinsHolder skinsHolder { get; private set; }
        [field: SerializeField] public BuyTypeIcons buyTypeIcons { get; private set; }
        [field: SerializeField] public ThemesHolder themesHolder { get; private set; }
        [field: SerializeField] public LevelsGoalConfig levelsGoalConfig { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            StartCoroutine(LoadScene());
        }

        private IEnumerator LoadScene()
        {
            var startTime = Time.time;
            
            yield return new WaitUntil(Ready);
            
            var elapsedTime = Time.time - startTime;
            
            yield return new WaitForSeconds(elapsedTime);

            SceneManager.LoadScene("Main");
        }

        private bool Ready()
        {
            return YandexGame.SDKEnabled;
        }
    }
}