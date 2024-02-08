using Assets.Scripts.Components.Audio;
using UnityEngine;

namespace Assets.Scripts.Ui
{
    public class Credits : MonoBehaviour
    {
        [SerializeField] private SwitchAudioComponent _sac;
        [SerializeField] private GameObject _gameObject;

        public void SetCreditsAudio() => _sac.SetCredits();

        public void StartCredits()
        {
            _gameObject.SetActive(true);
        }
    }
}
