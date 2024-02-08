using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Utils
{
    public class Timer 
    {
        [Min(0f)] private float _startTime, _currentTime;

        [field: Min(0f)] public float progress { get; private set; }

        public int minutes => (int)(_currentTime - secs) / 60;
        public int secs => (int)_currentTime % 60;

        public event Action OnTimerEnd;

        private async UniTaskVoid TimeCalculate() {
            while (_currentTime > 0) {
                _currentTime -= Time.deltaTime;
                progress = 1 - _currentTime / _startTime;
                
                await UniTask.Yield();
            }
            OnTimerEnd?.Invoke();
        }

        /// <summary>
        /// Запустить таймер.
        /// </summary>
        /// <param name="time"> Время в секундах.</param>
        public void StartTimer(float time) {
            _startTime = time;
            _currentTime = _startTime;
            
            Debug.Log($"Secs : {secs}, Minetes : {minutes}");
            
            TimeCalculate().Forget();
        }
        
    }
}