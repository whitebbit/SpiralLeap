﻿using _3._Scripts.Architecture;
using _3._Scripts.Architecture.Scriptable;
using UnityEngine;

namespace _3._Scripts
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioManager: Singleton<AudioManager>
    {
        [field: SerializeField] public SoundsConfig Config { get; private set; }
        private AudioSource _audioSource;
        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);
            
            _audioSource = GetComponent<AudioSource>();
        }

        public void PlayOneShot(AudioClip clip, float volume = 0.25f)
        {
            _audioSource.volume = volume;
            _audioSource.PlayOneShot(clip);
        }
    }
}