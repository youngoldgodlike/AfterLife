using Assets.Scripts.Utils;
using Cysharp.Threading.Tasks;
using Minions.WorkerMinion;
using UnityEngine;
using UnityEngine.AI;

namespace Minions.ProletariatStates
{
    internal class WorkingState : State<EWorkMinionState>
    {
        private readonly ProletariatMinion _minion;
        private readonly NavMeshAgent _agent;
        private readonly WorkPlace _work;
        private readonly Animator _animator;
        private static readonly int Work = Animator.StringToHash("Work");
        private static readonly int Idle = Animator.StringToHash("Idle");

        public WorkingState(EWorkMinionState key, ProletariatMinion worker, NavMeshAgent agent, WorkPlace workPlace,Animator animator) : base(key) {
            _minion = worker;
            _agent = agent;
            _work = workPlace;
            _animator = animator;
        }

        #region States

        public override void EnterState() {
            _agent.SetDestination(_work.location);
        }
        
        public override void UpdateState() {
            
        }

        public override void ExitState() {
            base.ExitState();
            _animator.SetTrigger(Idle);
        }

        #endregion

        #region Triggers
        public override void OnTriggerEnter(Collider other) {
            if (other.gameObject.IsInLayer(LayerMask.NameToLayer("WorkPlace"))) {
                other.TryGetComponent<WorkPlace>(out var workPlace);
                if (!(workPlace.GetOwner() == _minion.gameObject)) return;
                Working().Forget();
            }
        }

        public override void OnTriggerStay(Collider other) {
            
        }

        public override void OnTriggerExit(Collider other) {
            
        }
        #endregion
        
        private async UniTask Working() {
            _animator.SetTrigger(Work);
            var looking = _minion.StartCoroutine(_minion.LookAt(_work.extractionSite));
            await UniTask.WaitForSeconds(2f);
            _minion.StopCoroutine(looking);
        }
    }
}