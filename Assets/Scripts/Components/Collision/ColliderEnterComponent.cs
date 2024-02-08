using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Components
{
    public class ColliderEnterComponent : MonoBehaviour
    {
        [SerializeField] private string _tag;
        [SerializeField] private UnityEvent _action;

        private void OnCollisionEnter(Collision other)
        {
            if (other.collider.tag == _tag) 
                _action?.Invoke();
        }
    }
}
