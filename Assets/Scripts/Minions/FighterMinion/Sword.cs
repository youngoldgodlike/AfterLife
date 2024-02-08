using Enemy.ZombieBehaviors;
using UnityEngine;
using UnityEngine.Events;

namespace Minions.FighterMinion
{
    public class Sword : MonoBehaviour
    {
        public UnityEvent<GameObject> OnZombieSlain;
        private void OnTriggerEnter(Collider other) {
            Debug.Log($"Я ПОПАЛ ПО {other.name}");
            other.GetComponent<IEnemy>().Die();
        }
    }
}
