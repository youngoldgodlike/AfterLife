using UnityEngine;

public class ZombieWaypoint : MonoBehaviour
{
    [SerializeField] private MeshRenderer _mesh;
    [SerializeField, Range(0f, 10f)] private float _waitTime = 0f;
    public float WaitTime => _waitTime;
    private void Awake() {
        _mesh.enabled = false;
    }
}
