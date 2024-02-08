using Unity.VisualScripting;
using UnityEngine;

namespace Utils
{
    public abstract class PersistentSingleton<T> : MonoBehaviour where T : Component
    {
        public bool AutoUnparentOnAwake = true;
        
        protected static T instance;

        public static T Instance {
            get {
                if (instance.IsUnityNull()) {
                    instance = FindObjectOfType<T>();
                    if (instance.IsUnityNull()) {
                        var go = new GameObject(typeof(T).Name + "Auto-Generated");
                        instance = go.AddComponent<T>();
                    }
                }

                return instance;
            }
        }

        public static bool hasInstance => Instance != null;
        public static T TryGetInstance() => hasInstance ? Instance : null;

        protected virtual void Awake() {
            InitializeSingleton();
        }

        protected virtual void InitializeSingleton() {
            if(!Application.isPlaying) return;

            if (AutoUnparentOnAwake) transform.SetParent(null);
            
            if (instance == null) {
                instance = this as T;
                DontDestroyOnLoad(gameObject);
            } else {
                if (instance != this) {
                    Destroy(gameObject);
                }  
            }
        }
    }
}