using UnityEngine;

namespace Assets.Scripts.Components.Audio
{
    public class PlayRandomSoundsComponent : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip[] _clips;

        public void Play()
        {
            var random = new System.Random();
            
            _audioSource.PlayOneShot(_clips[random.Next(0, _clips.Length)]);
        }
    }
}