using Unity.VisualScripting;
using UnityEngine;

public class RotateComponent : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _rotateSpeed = 60f;

    private void Start() {
        if (_target.IsUnityNull())
            _target = transform;
    }

    private void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        transform.RotateAround(_target.position, Vector3.up, _rotateSpeed * Time.deltaTime);
    }
}