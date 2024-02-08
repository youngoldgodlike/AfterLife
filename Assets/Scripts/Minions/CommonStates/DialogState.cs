using System;
using System.Collections;
using Assets.Scripts.Ui;
using Ui;
using UnityEngine;

namespace Minions.CommonStates
{
    public class DialogState<TEState> : State<TEState> where TEState : Enum
    {
        private readonly Minion<TEState> _minion;
        private readonly DialogSystem _dialog;
        private bool _isDialogueEnd;

        public DialogState(TEState key, Minion<TEState> minion,DialogSystem dialogSystem) : base(key) {
            _minion = minion;
            _dialog = dialogSystem;
        }

        #region States

        public override void EnterState() {
            Debug.Log("Dialog just started. Wait to finish talk.");

            _dialog.DialogEnd += DialogEnd;
            _minion.DisableAgent();
            
            _minion.StartCoroutine(DialogueAction());
        }

        public override void ExitState() {
            base.ExitState();
            Debug.Log("Dialog is over");
            _dialog.DialogEnd -= DialogEnd;
            
            _isDialogueEnd = false;
            
            _minion.EnableAgent();
        }

        public override void UpdateState() {
            _minion.LookAtEyes(_minion.GetInterlocutor());
        }

        #endregion

        private IEnumerator DialogueAction() {
            yield return new WaitUntil(() => _isDialogueEnd);

            yield return new WaitForSeconds(1f);

            NextStateKey = _minion.defaultState;
        }

        private void DialogEnd() {
            _isDialogueEnd = true;
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