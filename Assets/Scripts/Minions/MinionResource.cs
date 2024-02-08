using UnityEngine;

namespace Minions
{
    public class MinionResource : MonoBehaviour
    {
        [SerializeField] private float _rotateSpeed = 60;
        [SerializeField] private Transform _parent;

        private void Start() {
            _rotateSpeed = 60f;
        }

        private void Update() {
            Rotate();
        }
    
        public void SetParent(Transform prnt) {
            _parent = prnt;
        }
    
        private void Rotate()
        {
            transform.RotateAround(_parent.position, Vector3.up, _rotateSpeed * Time.deltaTime);
        }
    }
}
