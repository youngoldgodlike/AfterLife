using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Components
{
    public class CheckOverlapResourceComponent : MonoBehaviour
    {
        [SerializeField] private string[] _tags;
        [SerializeField] private float _radius;
        
        [SerializeField] private LayerMask _layer;
        [SerializeField] private OnOverlapEvent _onOverlap = new();

        public void Check()
        {
            var size = Physics.OverlapSphere(transform.position, _radius, _layer);
            
            for (int i = 0; i < size.Length; i++)
            {
                var isInTags = _tags.Any(tag => size[i].CompareTag(tag));
                if (isInTags)  
                {
                   
                    _onOverlap?.Invoke(size[i].gameObject);
                    return;
                }
            }
        }
    
        [Serializable]
        public class  OnOverlapEvent : UnityEvent<GameObject>
        {
        }
    }
}
