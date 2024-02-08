using Cysharp.Threading.Tasks;
using Minions.WorkerMinion;
using UnityEngine;
using UnityEngine.AI;

namespace Minions.ProletariatStates
{
    internal class FreeState : State<EWorkMinionState>
    {
        private readonly ProletariatMinion _minion;
        private readonly NavMeshAgent _agent;

        public FreeState(EWorkMinionState key, ProletariatMinion minion, NavMeshAgent agent) : base(key) {
            _agent = agent;
            _minion = minion;
        }

        #region States

        public override void EnterState() {
            _minion.SetAgentMoveParameters(1.5f,240f);
        
            WalkingAround().Forget();
        }

        public override void ExitState() {
            base.ExitState();
        
            _minion.SetAgentMoveParameters();
        }

        public override void UpdateState() {
        
        }

        #endregion
    
        private async UniTask WalkingAround() {
            while (NextStateKey == StateKey) {
                _agent.SetDestination(_minion.GetWalkingAreaPoint());

                await UniTask.Yield();
                await UniTask.WaitUntil(() => _agent.remainingDistance < 1f);
                await UniTask.WaitForSeconds(5f);
            }
        }

        #region Triggers
    
        public override void OnTriggerEnter(Collider other) {
        }

        public override void OnTriggerStay(Collider other) {
        }

        public override void OnTriggerExit(Collider other) {
        }
    
        #endregion
    }
}