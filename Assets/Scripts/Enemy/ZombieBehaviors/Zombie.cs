using System;
using Assets.Scripts.Utils;
using Cysharp.Threading.Tasks;
using Enemy.ZombieBehaviors.States;
using StateMachine;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy.ZombieBehaviors
{
    public abstract class Zombie : StateMachine<ZombieState>, IEnemyTrigger, IEnemy
    {
        [Header("Base")]
        [SerializeField] protected Animator Animator;
        [SerializeField] protected NavMeshAgent Agent;
        [SerializeField] private bool _isMovingOnCutscene = true;

        [Header("Mics")]
        [SerializeField] protected ZombieState DefaultState;
        public ZombieState defaultState => DefaultState;
        [SerializeField] protected float WalkSpeed = 1f;
        public float walkSpeed => WalkSpeed;
        [SerializeField] protected float RunSpeed = 4f;
        public float runSpeed => RunSpeed;
        [SerializeField] private Transform _player;
        public Transform player => _player;
        [SerializeField] protected Vector3 StartPosition;
        public Vector3 startPosition => StartPosition;

        private static readonly int Death = Animator.StringToHash("Die");
        public event Action OnKilled;

        protected void Start() => StartPosition = transform.position;
        
        protected virtual async UniTask OnEnable() {
            await UniTask.WaitUntil(()=> GameEvents.TryGetInstance());
            GameEvents.Instance.OnReloadLevel += Restart;
            if (!_isMovingOnCutscene && ZombieNotifier.hasInstance) {
                ZombieNotifier.Instance.OnDialogEnd += Activate;
                ZombieNotifier.Instance.OnDialogStart += Stop;
            }
        }

        protected virtual void OnDisable() {
            GameEvents.Instance.OnReloadLevel -= Restart;
            if (ZombieNotifier.hasInstance) {
                ZombieNotifier.Instance.OnDialogEnd -= Activate;
                ZombieNotifier.Instance.OnDialogStart -= Stop;
            }
        }
        
        protected virtual void Restart() {
            Debug.Log($"Reset zombie {name}");
            TransitionToState(DefaultState);
            Agent.enabled = true;
            Agent.Warp(StartPosition);
            _player = null;
            OnKilled = null;
        }

        public void SetAgentParams(float speed,float angSpeed = 240f,float acceleration = 8f,float stopDist = 0f) {
            Agent.speed = speed;
            Agent.angularSpeed = angSpeed;
            Agent.acceleration = acceleration;
            Agent.stoppingDistance = stopDist;
        }
        
        public void Visit(Transform target) {
            _player = target;
            if (CurrentState.StateKey != ZombieState.Pursuit)
                TransitionToState(ZombieState.Pursuit);
        }

        public void Die() {
            gameObject.layer = default;
            Agent.speed = 0f;
            Animator.SetTrigger(Death);
            OnKilled?.Invoke();
            CurrentState = null;
            Destroy(gameObject,5f);
        }

        public void Attack() {
            Debug.Log("Player Killed");
            GameEvents.Instance.PlayerKilled();
        }

        private void Stop() {
            Agent.enabled = false;
            Animator.enabled = false;
        }

        private void Activate() {
            Agent.enabled = true;
            Animator.enabled = true;
        }
    }
}