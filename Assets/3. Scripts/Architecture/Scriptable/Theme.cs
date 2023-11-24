using UnityEngine;

namespace _3._Scripts.Architecture.Scriptable
{
    [CreateAssetMenu(fileName = "New Theme", menuName = "Configs/Themes/Theme")]
    public class Theme: ScriptableObject
    {
        [SerializeField] private Material ground;
        [SerializeField] private Material deadGround;
        [SerializeField] private Material cylinder;
        [SerializeField] private Transform background;
        [SerializeField] private Material skybox;

        public Material Ground => ground;
        public Material DeadGround => deadGround;
        public Material Cylinder => cylinder;
        public Transform Background => background;
        public Material Skybox => skybox;
    }
}