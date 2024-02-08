using Assets.Scripts.Utils;
using UnityEngine;
using UnityEngine.Events;

namespace Character.Stealth
{
    public class EnemyCheckingRing : MonoBehaviour
    {
        [SerializeField] private LayerMask _layer;
        [SerializeField] private UnityEvent _enemyEnter = new();
        [SerializeField] private UnityEvent _enemyExit = new();

        public void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.IsInLayer(_layer)) return;
            if (!other.TryGetComponent<IEnemyTrigger>(out IEnemyTrigger enemyVisitor)) return;
            
            _enemyEnter.Invoke();
            enemyVisitor.Visit(transform);
        }

        public void OnTriggerExit(Collider other) {
            
            if (!other.gameObject.IsInLayer(_layer)) return;
            if (!other.TryGetComponent<IEnemyTrigger>(out IEnemyTrigger enemyVisitor)) return;
            
            _enemyExit.Invoke();
        }
    }
}

