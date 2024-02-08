using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Ui
{
    public class TimerProgress : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _time;
        [SerializeField] private Image _progress;
        private Timer _timer;

        public void SetTimer(Timer timer) => _timer = timer;
        
        private void Update() {
            var minutes = _timer.minutes < 10 ? $"0{_timer.minutes}" : $"{_timer.minutes}";
            var secs = _timer.secs < 10 ? $"0{_timer.secs}" : $"{_timer.secs}";
            
            _time.text = $"{minutes}:{secs}";
            _progress.fillAmount = _timer.progress;
        }
    }
}
