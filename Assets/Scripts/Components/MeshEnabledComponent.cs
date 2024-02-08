using UnityEngine;

namespace Assets.Scripts.Components
{
    public class MeshDisableComponent : MonoBehaviour
    {
        [SerializeField] private MeshRenderer _mesh;

        private void Awake() => _mesh.enabled = false;
    }
}
