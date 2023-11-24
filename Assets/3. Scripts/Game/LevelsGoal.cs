using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _3._Scripts.Game
{
    [Serializable]
    public class LevelsGoal
    {
        [SerializeField] private int minLevel;
        [SerializeField] private int maxLevel;
        [Space] [SerializeField] private int minGoal;
        [SerializeField] private int maxGoal;
        
        public bool CorrectValue(int level)
        {
            return minLevel <= level && level <= maxLevel;
        }

        public int GetRandomValue()
        {
            return Random.Range(minGoal, maxGoal);
        }
    }
}