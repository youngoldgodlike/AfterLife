using System.Collections;
using Assets.Scripts.Utils;
using Cysharp.Threading.Tasks;
using Minions.WorkerMinion;
using UnityEngine;
using UnityEngine.AI;

namespace Minions
{
    public class CutsceneMinion : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private WorkPlace _work;

        private static readonly int Work = Animator.StringToHash("Work");
        private static readonly int Idle = Animator.StringToHash("Idle");
        private Vector3 position => transform.position;
    
        public void GetToWork(WorkPlace workPlace) {
            _work = workPlace;
            _work.SetOwner(gameObject);
            _agent.SetDestination(_work.location);
        }

        public void WorkDone() {
            _animator.SetTrigger(Idle);
        }
        
        private async UniTask Working() {
            _animator.SetTrigger(Work);
            var looking = StartCoroutine(LookAt(_work.extractionSite));
            await UniTask.WaitForSeconds(2f);
            StopCoroutine(looking);
        }
        
        public IEnumerator LookAt(Vector3 target) {
            while (true) {
                var direction = target - transform.position;
                direction = Vector3.ProjectOnPlane(direction, Vector3.up);
                
                var lookDir = Quaternion.LookRotation(direction);

                var newRot = Quaternion.Slerp(transform.rotation, lookDir, 0.17f);
                transform.rotation = newRot;

                Debug.DrawRay(position, direction, Color.red);
                Debug.DrawRay(position, transform.forward, Color.green);
                
                yield return null;
            }
        }

        public void SetDestination(Transform dest) {
            _agent.SetDestination(dest.position);
        }

        public void OnTriggerEnter(Collider other) {
            if (other.gameObject.IsInLayer(LayerMask.NameToLayer("WorkPlace"))) {
                other.TryGetComponent<WorkPlace>(out var workPlace);
                if (!(workPlace.GetOwner() == gameObject)) return;
                Working().Forget();
            }
        }
    }
}
