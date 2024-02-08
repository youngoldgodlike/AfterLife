using System;
using Assets.Scripts.Components.Audio;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Altar
{
    public class AltarAnimation : MonoBehaviour
    {
        [SerializeField] private float _delay;
        [SerializeField] private ParticlesData[] _particles;
        [SerializeField] private UnityEvent _onEndAnimation;
        
        private PlayAudioComponent _audio;

        private void Awake()
        {
            _audio = GetComponent<PlayAudioComponent>();
        }

        public void CompleteBuild() => StartAnimation().Forget();
        
        [ContextMenu("Start")]
        private async UniTaskVoid  StartAnimation()
        {
            for (int i = 0; i < _particles.Length; i++)
            {
                ShowEffects(_particles[i]);
                _audio?.Play();

                if (i != _particles.Length - 1)
                    await UniTask.Delay(TimeSpan.FromSeconds(_delay));
            }
           
            _onEndAnimation?.Invoke();
        }

        private void ShowEffects(ParticlesData go)
        {
            foreach (var item in go.particles)
            {
                item.SetActive(true);
            }
        }

        [Serializable]
        private class ParticlesData
        {
            public GameObject[] particles;
        }

    }
}