using System.Collections.Generic;
using System.Linq;
using _3._Scripts.Game;
using UnityEngine;
using YG;

namespace _3._Scripts.Architecture.Scriptable
{
    [CreateAssetMenu(fileName = "LevelsGoalConfig", menuName = "Configs/Game/Levels Goal Config")]
    public class LevelsGoalConfig: ScriptableObject
    {
        [SerializeField] private List<LevelsGoal> levelsGoals = new List<LevelsGoal>();

        public int GetGoal()
        {
            var level = YandexGame.savesData.currentLevel;
            var goal = levelsGoals.FirstOrDefault(l => l.CorrectValue(level));
            return goal?.GetRandomValue() ?? 0;
        }
    }
}