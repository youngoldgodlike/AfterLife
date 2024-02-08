using Character.Stealth;
using UnityEngine;

namespace Enemy.ZombieBehaviors.States
{
    internal class Unleashed : ICharacterBehaviour
    {
        public void Set(Transform character) {
            var movement = character.GetComponent<MineCharacterController>();
            var circle = character.GetComponentInChildren<DrawCircleStealth>();
            movement.Unleash();
            circle.gameObject.layer = 20;
            character.gameObject.layer = 3;
        }
    }
}