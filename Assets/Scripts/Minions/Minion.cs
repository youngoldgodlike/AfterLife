using System;
using System.Collections;
using Assets.Scripts.Ui;
using Character.CharacterBehaviours;
using Cysharp.Threading.Tasks;
using StateMachine;
using Ui;
using UnityEngine;
using UnityEngine.AI;

namespace Minions
{
    public abstract class Minion<TEState> : StateMachine<TEState> where TEState : Enum
    {
        [Header("Move Parameters")] 
        [SerializeField] protected float Speed = 3.5f;
        [SerializeField] protected float AngularSpeed = 360f;
        [SerializeField] protected float Acceleration = 16f;
        [SerializeField] protected float StoppingDistance = 0f;
        
        [Header("Misc")]
        [SerializeField] protected Animator GhostAnimator;
        [SerializeField] protected Transform Interlocutor;
        [SerializeField] protected NavMeshAgent agent;
        [SerializeField] protected DialogSystem Dialog;
        [SerializeField] protected TEState DefaultState;

        public TEState defaultState => DefaultState;
        
        protected void Start() {
            Dialog = DialogSystem.Instance;
            Interlocutor = CharacterBehaviour.Instance.transform;
        }

        #region Minion Actions For States

        // смотреть на игрока
        public void LookAtEyes(Vector3 target) {
            Vector3 direction = target - transform.position;
            
            transform.rotation = Quaternion.LookRotation(direction);
        }

        public IEnumerator LookAt(Vector3 target) {
            while(true) {
                var direction = target - transform.position;
                direction = Vector3.ProjectOnPlane(direction, Vector3.up);
                
                var lookDir = Quaternion.LookRotation(direction);

                var newRot = Quaternion.Slerp(transform.rotation, lookDir, 0.17f);
                transform.rotation = newRot;

                Debug.DrawRay(transform.position,direction,Color.red);
                Debug.DrawRay(transform.position, transform.forward, Color.green);
                
                yield return null;
            }
        }

        // настройка движения
        public void SetAgentMoveParameters(float speed = 3.5f, float angularSpeed = 360f, float acceleration = 32f, float stoppingDistance = 0.1f) {
            agent.speed = speed;
            agent.angularSpeed = angularSpeed;
            agent.acceleration = acceleration;
            agent.stoppingDistance = stoppingDistance;
        }

        // выкл/вкл агента
        public void DisableAgent() {
            agent.enabled = false;
        }
        public void EnableAgent() {
            agent.enabled = true;
        }
        public Vector3 GetInterlocutor() => Interlocutor.position;
        
        #endregion

        public abstract void BeginningDialog(Transform player, DialogSystem dialogSystem);
        
    }
}

