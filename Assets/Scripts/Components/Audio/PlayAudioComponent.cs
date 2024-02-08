using UnityEngine;

namespace Assets.Scripts.Components.Audio
{
    public class PlayAudioComponent : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _audioClip;

        public void Play() => _audioSource.PlayOneShot(_audioClip);
    
    }
}
