using KinematicCharacterController;
using Minions.FighterMinion;
using UnityEngine;
using UnityEngine.AI;

namespace Minions.FighterStates
{
    public class FollowPlayerState: State<EMeleeSoldierState>
    {
        private readonly MinionFighter _minion;
        private readonly NavMeshAgent _agent;
        private readonly Animator _swordAnimator;
        private static readonly int Idle = Animator.StringToHash("Idle");

        public FollowPlayerState(EMeleeSoldierState key,MinionFighter minion,NavMeshAgent agent, Animator swordAnimator) : base(key) {
            _minion = minion;
            _agent = agent;
            _swordAnimator = swordAnimator;
        }

        #region States

        public override void EnterState() {
            _minion.SetAgentMoveParameters(3,360,8,2.5f);
            //_swordAnimator.SetTrigger(Idle);
        }

        public override void UpdateState() {
            _agent.SetDestination(_minion.player);
        }

        #endregion
    }
}