using System;
using Assets.Scripts.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Data
{
    public class SliderSensitivitySettingWidget : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private string _tag;
        
        public delegate void OnAction();

        private event OnAction onValueChange;
        
        private void Awake()
        {
            _slider.onValueChanged.AddListener((param) =>
            {
                SetValue(param, _tag);
                onValueChange?.Invoke();
            });
        }

        private void Start()
        {
            _slider.value = PlayerPrefs.GetFloat(_tag, 0.5f);
            SetText(_slider.value);
        }
        
        public void Subscribe(OnAction action) => onValueChange += action;
        
        private void SetValue(float param, string tag)
        {
            SetText(_slider.value);
            var value = Mathf.Clamp(param, 0.1f, 1f);
            PlayerPrefs.SetFloat(tag, value);
        }
        
        private void SetText(float value)
        {
            var percent = Math.Round(value, 1);
            
            _text.text = Mathf.Clamp((float)percent, 0.1f, 1f).ToString();
        }
    }
}
