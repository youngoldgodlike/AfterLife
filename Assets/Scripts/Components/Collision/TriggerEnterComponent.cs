using Assets.Scripts.Utils;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Components
{
    public class TriggerEnterComponent : MonoBehaviour
    {
        [SerializeField] private LayerMask _layer;
        [SerializeField] private string _tag;
        
        [SerializeField] private UnityEvent _onEnter;
        [SerializeField] private UnityEvent _onStay;
        [SerializeField] private UnityEvent _onExit;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.IsInLayer(_layer) && other.gameObject.tag != _tag) return;
            
            _onEnter?.Invoke();
        }

        private void OnTriggerStay(Collider other)
        {
            if (!other.gameObject.IsInLayer(_layer) && other.gameObject.tag != _tag) return;
            
            _onStay?.Invoke();
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.gameObject.IsInLayer(_layer) && other.gameObject.tag != _tag) return;
            
            _onExit?.Invoke();
        }
    }
}
