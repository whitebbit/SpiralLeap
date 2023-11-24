using System;
using _3._Scripts.Architecture.Enums;
using UnityEngine;

namespace _3._Scripts.Architecture.Scriptable
{
    [CreateAssetMenu(fileName = "BuyTypeIcons", menuName = "Configs/UI/Buy Type Icons")]
    public class BuyTypeIcons: ScriptableObject
    {
        [SerializeField] private Sprite coins;
        [SerializeField] private Sprite ad;
        [SerializeField] private Sprite premium;

        public Sprite GetIcon(BuyType type)
        {
            return type switch
            {
                BuyType.Coins => coins,
                BuyType.Ad => ad,
                BuyType.None => null,
                BuyType.Premium => premium,
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }
    }
}