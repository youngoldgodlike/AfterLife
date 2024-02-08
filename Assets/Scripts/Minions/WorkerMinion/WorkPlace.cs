using UnityEngine;
using UnityEngine.Events;

namespace Minions.WorkerMinion
{
    public class WorkPlace : MonoBehaviour
    {
        [SerializeField] private Transform _extractionSite;
        [SerializeField] private GameObject _owner;
        
        public UnityEvent OnDestroyPlace = new();
        public Vector3 location => transform.position;
        public Vector3 extractionSite => _extractionSite.position;

        public void SetOwner(GameObject owner) => _owner = owner;

        private void OnDestroy() {
            OnDestroyPlace.Invoke();
        }

        public GameObject GetOwner() => _owner;
    }
}