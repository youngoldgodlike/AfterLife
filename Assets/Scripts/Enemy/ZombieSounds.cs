using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSounds : MonoBehaviour
{
    [SerializeField] private AudioSource _soundAudioSource;

    [SerializeField] private AudioClip _scream;
    [SerializeField] private AudioClip _punch;
    
    public void Scream() {
        _soundAudioSource.PlayOneShot(_scream);
    }

    public void Punch() {
        _soundAudioSource.PlayOneShot(_punch);
    }
}
