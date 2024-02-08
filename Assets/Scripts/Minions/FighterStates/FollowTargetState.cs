using Minions.FighterMinion;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

namespace Minions.FighterStates
{
    public class FollowTargetState : State<EMeleeSoldierState>
    {
        private readonly MinionFighter _minion;
        private readonly NavMeshAgent _agent;
        private readonly TargetSearcher _searcher;
        private readonly Animator _swordAnimator;
        private static readonly int Attack = Animator.StringToHash("Attack");
        private static readonly int PrepareAttack = Animator.StringToHash("PrepareAttack");
        private static readonly int SheatheSword = Animator.StringToHash("SheatheSword");

        public FollowTargetState(EMeleeSoldierState key,MinionFighter minion,NavMeshAgent agent,TargetSearcher searcher, Animator swordAnimator) : base(key) {
            _minion = minion;
            _agent = agent;
            _searcher = searcher;
            _swordAnimator = swordAnimator;
        }

        #region States

        public override void EnterState() {
            _minion.SetAgentMoveParameters(5f,360f,16f,1.5f);
            _swordAnimator.SetTrigger(PrepareAttack);
            _swordAnimator.ResetTrigger(SheatheSword);
            _swordAnimator.ResetTrigger(Attack);
        }

        public override void UpdateState() {
            if (!_searcher.Target.IsUnityNull()) {
                _agent.SetDestination(_searcher.Target.position);
                
                if(_agent.remainingDistance < 8) _swordAnimator.SetTrigger(Attack);
            }
            else NextStateKey = EMeleeSoldierState.FollowPlayer;
        }

        public override void ExitState() {
            base.ExitState();
            _swordAnimator.SetTrigger(SheatheSword);
            _swordAnimator.ResetTrigger(PrepareAttack);
            _swordAnimator.ResetTrigger(Attack);
        }

        #endregion
    }
}