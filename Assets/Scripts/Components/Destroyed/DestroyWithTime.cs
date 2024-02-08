using UnityEngine;

namespace Assets.Scripts.Components
{
    public class DestroyWithTimeComponent : MonoBehaviour
    {
        [SerializeField] private float _timeToDestroy;
        private void Start() => Destroy(gameObject, _timeToDestroy);
    
    }
}
