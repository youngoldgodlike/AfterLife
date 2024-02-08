using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy.ZombieBehaviors.States
{
    public class Staying : State<ZombieState>
    {
        private readonly Zombie _zombie;
        private readonly NavMeshAgent _agent;
        private readonly Animator _animator;

        private static readonly int Idle = Animator.StringToHash("Idle");
        private static readonly int Walk = Animator.StringToHash("Walk");

        #region Zombie fields
        private Vector3 position => _zombie.transform.position;
        #endregion
        
        public Staying(ZombieState key, Zombie zombie, NavMeshAgent agent, Animator animator) : base(key) {
            _zombie = zombie;
            _agent = agent;
            _animator = animator;
        }

        public override void EnterState() {
            var dist = Vector3.Distance(_zombie.startPosition, position);
            if (dist > 1f)
                BackToStandPlace().Forget();
            _animator.SetTrigger(Idle);
        }

        public override void UpdateState() {
        }

        private async UniTask BackToStandPlace() {
            _zombie.SetAgentParams(_zombie.walkSpeed, stopDist: 0f);
            _agent.SetDestination(_zombie.startPosition);
            _animator.SetTrigger(Walk);

            await UniTask.Yield();
            await UniTask.WaitUntil(() => _agent.remainingDistance < 0.1f);
            _animator.ResetTrigger(Walk);
            
            _animator.SetTrigger(Idle);
        }
    }
}