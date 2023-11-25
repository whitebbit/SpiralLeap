using UnityEngine;

namespace _3._Scripts.Architecture.Scriptable
{
    [CreateAssetMenu(fileName = "SoundsConfig", menuName = "Configs/Visual/SoundsConfig")]
    public class SoundsConfig: ScriptableObject
    {
        [field:SerializeField] public AudioClip UIClick { get; private set; }
        [field:SerializeField] public AudioClip OnReward { get; private set; }
        [field:SerializeField] public AudioClip OnWin { get; private set; }
        [field:SerializeField] public AudioClip OnLose { get; private set; }
        [field:SerializeField] public AudioClip UnlockLevel { get; private set; }
        [field:SerializeField] public AudioClip BuySkin { get; private set; }
    }
}