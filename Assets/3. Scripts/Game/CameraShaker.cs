using _3._Scripts.Architecture;
using DG.Tweening;
using UnityEngine;

namespace _3._Scripts.Game
{
    public class CameraShaker : Singleton<CameraShaker>
    {
        [SerializeField] private float duration;
        [SerializeField] private float strength = 1f;
        [SerializeField] private int vibrato = 10;
        [SerializeField] private float randomness = 90f;
        [SerializeField] private bool snapping = false;
        [SerializeField] private bool fadeOut = true;

        private Tween _tween;

        public void StartShake()
        {
            if(_tween != null)return;
            _tween = transform.DOShakePosition(duration, strength, vibrato, randomness, snapping, fadeOut).SetLoops(-1);
        }

        public void StopShake()
        {
            _tween.Pause();
            _tween.Kill();
            _tween = null;
            transform.DOLocalMove(Vector3.zero, 0.25f);
        }
    }
}