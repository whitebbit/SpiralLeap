using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _3._Scripts.Architecture.Scriptable
{    
    [CreateAssetMenu(fileName = "New ThemesHolder", menuName = "Configs/Themes/ThemesHolder")]
    public class ThemesHolder: ScriptableObject
    {
        [SerializeField] private List<Theme> themes;

        public List<Theme> Themes => themes;

        public Theme GetRandomTheme()
        {
            var rand = Random.Range(0, themes.Count);
            return themes[rand];
        }
    }
}