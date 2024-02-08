using System;
using UnityEngine;
using Utils;

namespace Assets.Scripts.Utils
{
    public class GameEvents : Singleton<GameEvents>
    {
        public event Action OnReloadLevel;
        public event Action OnPlayerKilled;

        public void PlayerKilled() {
            OnPlayerKilled?.Invoke();
        }
        
        public bool ReloadLevel() {
            Debug.Log("RELOADING START");
            OnReloadLevel?.Invoke();

            return true;
        }
    }
}
