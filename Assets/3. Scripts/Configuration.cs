using System;
using System.Threading.Tasks;
using _3._Scripts.Architecture;
using _3._Scripts.Architecture.Scriptable;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _3._Scripts
{
    public class Configuration: Singleton<Configuration>
    {
        [field:SerializeField] public SkinsHolder skinsHolder { get; private set; }
        [field:SerializeField] public BuyTypeIcons buyTypeIcons { get; private set; }
        [field:SerializeField] public ThemesHolder themesHolder { get; private set; }
        [field:SerializeField] public LevelsGoalConfig levelsGoalConfig { get; private set; }
        
        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);
        }

        private async void Start()
        {
            await Task.Delay(1500);
            SceneManager.LoadScene(1);
        }
    }
}