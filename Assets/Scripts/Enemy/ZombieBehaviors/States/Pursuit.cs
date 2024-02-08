using Character.CharacterBehaviours;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy.ZombieBehaviors.States
{
    internal class Pursuit : State<ZombieState>
    {
        private readonly Zombie _zombie;
        private readonly Animator _animator;
        private readonly NavMeshAgent _agent;
        private static readonly int Moving = Animator.StringToHash("Pursuit");
        private static readonly int Velocity = Animator.StringToHash("Velocity");
        private static readonly int Attack = Animator.StringToHash("Attack");
        private bool _isCatchPlayer;

        public Pursuit(ZombieState key, Zombie zombie, NavMeshAgent agent, Animator animator) : base(key) {
            _zombie = zombie;
            _agent = agent;
            _animator = animator;
        }

        public override void EnterState() {
            _animator.SetTrigger(Moving);
            _zombie.SetAgentParams(_zombie.runSpeed ,acceleration:16f,stopDist:2f);
        }

        public override void UpdateState() {
            if (_agent.enabled) {
                _agent.SetDestination(_zombie.player.position);
                var velocity = _agent.velocity.magnitude / _agent.speed;
                _animator.SetFloat(Velocity, velocity);
            }
        }

        public override void ExitState() {
            base.ExitState();
            _isCatchPlayer = false;
        }

        public override void OnTriggerEnter(Collider other) {
            if (!other.gameObject.CompareTag("Character")) return;
            _isCatchPlayer = true;
            
            other.GetComponent<CharacterBehaviour>().SetBehaviour(new Caught(_zombie.transform.position));
            
            _zombie.OnKilled += () => {
                other.GetComponent<CharacterBehaviour>().SetBehaviour(new Unleashed());
            };

            _agent.enabled = false;
            _animator.SetTrigger(Attack);
        }

        public override void OnTriggerExit(Collider other) {
            if(_isCatchPlayer) return;
            _agent.enabled = true;
            NextStateKey = _zombie.defaultState;
        }
    }
}