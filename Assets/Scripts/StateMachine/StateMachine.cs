using System;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine
{
    public class StateMachine<TEState> : MonoBehaviour where TEState : Enum
    {
        protected Dictionary<TEState, State<TEState>> States = new();
        protected State<TEState> CurrentState;

        protected void Update() {
            if (CurrentState == null) return;

            var nextStateKey = CurrentState.GetNextState();

            if (nextStateKey.Equals(CurrentState.StateKey)) {
                CurrentState.UpdateState();
            }
            else {
                TransitionToState(nextStateKey);
            }
        }

        protected void TransitionToState(TEState key) {
            CurrentState?.ExitState();
            CurrentState = States[key];
            CurrentState.EnterState();
        }

        private void OnTriggerEnter(Collider other) {
            CurrentState?.OnTriggerEnter(other);
            TriggerEnter(other);
        }

        private void OnTriggerStay(Collider other) {
            CurrentState?.OnTriggerStay(other);
        }

        private void OnTriggerExit(Collider other) {
            CurrentState?.OnTriggerExit(other);
        }

        protected virtual void TriggerEnter(Collider other){}
    }
}