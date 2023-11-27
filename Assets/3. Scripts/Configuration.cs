using System;
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
#if !UNITY_EDITOR
            Application.targetFrameRate = 60;
#endif
        }

        private void Start()
        {
            StartCoroutine(LoadScene());
        }

        private IEnumerator LoadScene()
        {
            while (!YandexGame.SDKEnabled)
            {
                Debug.Log($"YandexGame.SDKEnabled is {YandexGame.SDKEnabled}");
                yield return null;
            }

            SceneManager.LoadScene("Main");
        }
    }
}