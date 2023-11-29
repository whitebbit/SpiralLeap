using System;
using UnityEngine;

namespace _3._Scripts.Architecture.Scriptable
{
    [CreateAssetMenu(fileName = "New Theme", menuName = "Configs/Themes/Theme")]
    public class Theme : ScriptableObject
    {
        [Header("Main objects")] [SerializeField]
        private Material ground;

        [SerializeField] private Material deadGround;
        [SerializeField] private Material cylinder;

        [Header("Environment")] [SerializeField]
        private Transform background;
        [SerializeField] private Material skybox;

        [SerializeField] private FogSetting fogSetting;

        public Material Ground => ground;
        public Material DeadGround => deadGround;
        public Material Cylinder => cylinder;
        public Transform Background => background;
        public Material Skybox => skybox;
        public FogSetting FogSetting => fogSetting;
    }

    [Serializable]
    public class FogSetting
    {
        [Header("Background")] 
        public bool enableBackgroundFog;
        public Color backgroundFog;

        public float startValue;
        public float endValue;
        [Header("Floor")] public Color floorFog;
    }
}