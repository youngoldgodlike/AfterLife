using UnityEngine;

namespace Assets.Scripts.Components.Audio
{
    public class SwitchAudioComponent : MonoBehaviour
    {
        [SerializeField] private AudioClip _aram;
        [SerializeField] private AudioClip _default;
        [SerializeField] private AudioClip _credits;

        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        [ContextMenu("SetAram")]
        public void SetAram()
        {
            _audioSource.clip = _aram;
            _audioSource.Play();
        }

        public void SetDefault()
        {
            _audioSource.clip = _default;
            _audioSource.Play();
        } 
        public void SetCredits()
        {
            _audioSource.clip = _credits;
            _audioSource.Play();
        }




    }
}
