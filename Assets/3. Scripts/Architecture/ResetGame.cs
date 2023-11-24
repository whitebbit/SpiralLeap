using UnityEditor;
using UnityEngine;
using YG;

namespace _3._Scripts.Architecture
{
#if UNITY_EDITOR
    public class ResetGame: Editor
    {
        [MenuItem("Tools/Reset Game")]
        private static void Resenting()
        {
            YandexGame.ResetSaveProgress();
            YandexGame.SaveProgress();
        }
    }
#endif
}