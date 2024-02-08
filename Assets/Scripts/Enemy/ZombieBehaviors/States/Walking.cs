using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Utils;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using Utils;

namespace Enemy.ZombieBehaviors.States
{
    public class Walking : State<ZombieState>
    {
        private readonly WalkingZombie _zombie;
        private readonly Animator _animator;
        private readonly NavMeshAgent _agent;
        private int _wpIndex;

        private static readonly int Moving = Animator.StringToHash("Walk");
        private static readonly int Velocity = Animator.StringToHash("Velocity");

        private Coroutine _walkMode;


        #region Zombie fields
        private List<Transform> waypoints => _zombie.waypoints;

        private Vector3 position {
            get => _zombie.transform.position;
            set => _zombie.transform.position = value;
        }
        #endregion

        public Walking(ZombieState key, WalkingZombie zombie, NavMeshAgent agent, Animator animator) : base(key) {
            _zombie = zombie;
            _agent = agent;
            _animator = animator;
            _wpIndex = 0;

            GameEvents.Instance.OnReloadLevel += ResetWay;
        }

        ~Walking() {
            GameEvents.Instance.OnReloadLevel -= ResetWay;
        }

        private void ResetWay() => _wpIndex = 0;

        public override void EnterState() {
            _zombie.SetAgentParams(_zombie.walkSpeed,stopDist:0.3f);
            _animator.SetTrigger(Moving);
            if (!(waypoints.Count < 2))
                _walkMode = _zombie.StartCoroutine(FollowToPoint());
        }

        public override void UpdateState() {
            var velocity = _agent.velocity.magnitude / _agent.speed;
            _animator.SetFloat(Velocity, velocity);
        }

        public override void ExitState() {
            base.ExitState();
            _zombie.StopCoroutine(_walkMode);
        }
        private IEnumerator FollowToPoint() {
            while (true) {
                _agent.destination = waypoints[_wpIndex].position;
                yield return null;
                
                yield return new WaitUntil(() => {
                    if (_agent.enabled)
                        return _agent.remainingDistance <= _agent.stoppingDistance;
                    return false;
                });
                
                var waitingTime = waypoints[_wpIndex].GetComponent<ZombieWaypoint>().WaitTime;
                if (_wpIndex == waypoints.Count - 1) _wpIndex = 0;
                else _wpIndex++;
                
                yield return new WaitForSeconds(waitingTime);
            }
        }
    }
}