using System.Collections.Generic;
using System.Linq;
using _3._Scripts.Architecture.Enums;
using UnityEngine;

namespace _3._Scripts.Architecture.Scriptable
{
    [CreateAssetMenu(fileName = "Skins Holder", menuName = "Configs/Skins/SkinsHolder")]
    public class SkinsHolder: ScriptableObject
    {
        [SerializeField] private List<Skin> skins;

        public List<Skin> Skins => skins;

        public Skin GetSkinByType(BuyType type)
        {
            return skins.FirstOrDefault(s => s.BuyType == type);
        }
        public Skin GetSkin(string name)
        {
            return skins.FirstOrDefault(s => s.Name == name);
        }
    }
}