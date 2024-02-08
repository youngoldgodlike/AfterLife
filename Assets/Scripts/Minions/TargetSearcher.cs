using System;
using System.Collections.Generic;
using Minions.FighterMinion;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace Minions
{
    public class TargetSearcher : MonoBehaviour
    {
        [SerializeField] private LayerMask _obstructions;
        [SerializeField] private LayerMask _targetLayer;
        [SerializeField] private List<Transform> _targets;
        [SerializeField] private Sword _sword;

        [NonSerialized] public UnityEvent OnTargetDetected = new();
        private Vector3 Position => transform.position;
        public Transform Target {
            get {
                if (_targets.Count > 0) return _targets[0];
                return null;
            }
        }

        private void Awake() {
            _sword.OnZombieSlain.AddListener((target) => {
                if (_targets.Contains(target.transform)) _targets.Remove(target.transform);
            });
        }

        private void Update() {
            if (Target.IsUnityNull()) _targets.Remove(Target);
        }
        
        private bool IsInSight(Transform target) {
            var dir = target.position - Position;
            var distance = Vector3.Distance(Position, target.position);
            var inSight = !Physics.Raycast(Position, dir, distance, _obstructions);

            //Debug.Log($"Is {target.name} on view: {inSight}");
            return inSight;
        }

        private bool CheckAndTryAddTarget(Transform target) {
            if (IsInSight(target)) {
                _targets.Add(target);
                if (_targets.Count == 1) OnTargetDetected.Invoke();
                return true;
            }
            return false;
        }
        
        private void OnTriggerEnter(Collider other) {
            if (_targets.Contains(other.transform)) return;

            CheckAndTryAddTarget(other.transform);
        }

        private void OnTriggerStay(Collider other) {
            if (_targets.Contains(other.transform)) return;
            
            CheckAndTryAddTarget(other.transform);
        }

        private void OnTriggerExit(Collider other) {
            if(_targets.Contains(other.transform))
                _targets.Remove(other.gameObject.transform);
        }
    }
}