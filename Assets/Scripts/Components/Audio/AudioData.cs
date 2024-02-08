using System;
using UnityEngine;

namespace Assets.Scripts.Components.Audio
{
    [Serializable]
    public class AudioData 
    {
        [SerializeField] private string _id;
        [SerializeField] private AudioClip _clip;

        public string Id => _id;
        public AudioClip Clip => _clip;
    }
}