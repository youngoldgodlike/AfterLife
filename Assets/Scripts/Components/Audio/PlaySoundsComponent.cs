using System;
using UnityEngine;

namespace Assets.Scripts.Components.Audio
{
    public class PlaySoundsComponent : MonoBehaviour
    {
        [SerializeField] private AudioSource _source;
        [SerializeField] private AudioData[] _audio;

        public void Play(string id)
        {
            foreach (var item in _audio)
            {
                if (item.Id != id) continue;
                
                _source.PlayOneShot(item.Clip);
                break;
            }
        }
        
        
        [Serializable]
        public class AudioData 
        {
            [SerializeField] private string _id;
            [SerializeField] private AudioClip _clip;

            public string Id => _id;
            public AudioClip Clip => _clip;
        }
    }

    

}