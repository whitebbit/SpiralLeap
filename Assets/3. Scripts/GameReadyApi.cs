using System;
using UnityEngine;
using YG;

namespace _3._Scripts
{
    public class GameReadyApi: MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            YandexGame.GameReadyAPI();
        }
    }
}