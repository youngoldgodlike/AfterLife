using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Enemy.ZombieBehaviors.States;
using UnityEngine;

namespace Enemy.ZombieBehaviors
{
    public class WalkingZombie : Zombie
    {
        [Header("Settings")]
        [SerializeField] private Transform _waypointsParent;
        [SerializeField] private RuntimeAnimatorController _zombieSeek;
        public List<Transform> waypoints { get; private set; }

        protected override async UniTask OnEnable() {
            await base.OnEnable();
            if(ZombieNotifier.hasInstance)
                ZombieNotifier.Instance.OnDiamondPickedUp += TurnAngryMode;
        }

        protected override void OnDisable() {
            base.OnDisable();
            ZombieNotifier.Instance.OnDiamondPickedUp -= TurnAngryMode;
        }

        private void Awake() {
            var points = _waypointsParent.GetComponentsInChildren<Transform>();
            waypoints = points.ToList();
            waypoints.Remove(_waypointsParent);
        }

        private new void Start() {
            base.Start();
            States.Add(ZombieState.Walking, new Walking(ZombieState.Walking, this, Agent, Animator));
            States.Add(ZombieState.Pursuit, new Pursuit(ZombieState.Pursuit, this, Agent, Animator));
            
            CurrentState = States[ZombieState.Walking];
            CurrentState.EnterState();
        }

        private void TurnAngryMode() {
            Animator.runtimeAnimatorController = _zombieSeek;
            WalkSpeed = 5f;
            RunSpeed = 6f;
            TransitionToState(ZombieState.Walking);
        }
    }
}