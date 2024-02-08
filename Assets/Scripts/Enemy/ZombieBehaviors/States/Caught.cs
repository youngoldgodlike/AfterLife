using Character.Stealth;
using UnityEngine;

namespace Enemy.ZombieBehaviors.States
{
    internal class Caught : ICharacterBehaviour
    {
        private readonly Vector3 _enemyPos;
        public Caught(Vector3 enemyPos) {
            _enemyPos = enemyPos;
        }

        public void Set(Transform character) {
            var movement = character.GetComponent<MineCharacterController>();
            var circle = character.GetComponentInChildren<DrawCircleStealth>();
            movement.Caught(_enemyPos);
            circle.gameObject.layer = 0;
            character.gameObject.layer = 0;
        }
    }
}