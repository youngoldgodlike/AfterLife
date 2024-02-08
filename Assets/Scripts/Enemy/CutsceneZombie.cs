using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace Enemy
{
    public class CutsceneZombie : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private Animator _animator;
        [SerializeField] private UnityEvent OnZombieEnabling = new();
        [SerializeField] private bool _isRun;
        private static readonly int Run = Animator.StringToHash("Run");
        private static readonly int Walk = Animator.StringToHash("Walk");

        private void Awake() {
            if (_agent.IsUnityNull()) _agent = GetComponent<NavMeshAgent>();
        }

        public void OnEnable() {
            OnZombieEnabling.Invoke();
        }

        public void GoTo(Transform dest) {
            _agent.SetDestination(dest.position);
            _animator.SetTrigger(_isRun ? Run : Walk);
        }
    }
}
