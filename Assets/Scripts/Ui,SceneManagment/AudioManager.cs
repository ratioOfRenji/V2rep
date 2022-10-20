using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TestProject
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField]
        AudioSource _musicSource;
        [SerializeField]
        AudioSource _loseAudioSource;
        [SerializeField]
        AudioSource _winAudioSource;
        [SerializeField]
        Slider _audioVolumeSlider;
        private void Update()
        {
            _musicSource.volume = _audioVolumeSlider.value;
        }
        public void PlayLoseSound()
        {
            _musicSource.Stop();
            _loseAudioSource.Play();
        }
        public void PlayWinSound()
        {
            _musicSource.Stop();
            _winAudioSource.Play();
        }
    }
}
