using _3._Scripts.Architecture;
using _3._Scripts.Architecture.Scriptable;
using Plugins.Audio.Core;
using UnityEngine;

namespace _3._Scripts
{
    public class AudioManager: Singleton<AudioManager>
    {
        [SerializeField] private SourceAudio musicAudioSource;
        private SourceAudio _audioSource;
        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);
            
            _audioSource = GetComponent<SourceAudio>();
        }

        public void PlayOneShot(string key, float volume = 0.25f)
        {
            _audioSource.Volume = volume;
            _audioSource.PlayOneShot(key);
        }

        public void MusicState(bool state)
        {
            
            if(state)
            {
                if(!musicAudioSource.IsPlaying)
                    musicAudioSource.Play("background");
            } 
            else musicAudioSource.Stop();
        }
    }
}