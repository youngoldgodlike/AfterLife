using Assets.Scripts.Utils;
using UnityEngine;

namespace Utils
{
    [RequireComponent(typeof(BoxCollider))]
    public class CheckPoint : MonoBehaviour
    {
        [SerializeField] private Transform _checkPointTransform;

        private void Awake() {
            _checkPointTransform.GetComponent<MeshRenderer>().enabled = false;
        }

        private void OnTriggerEnter(Collider other) {
            CheckPointManager.Instance.SetCheckPoint(_checkPointTransform);
        }
    }
}