using System;
using UnityEngine;

public class RotateTowardsComponent : MonoBehaviour
{
    [SerializeField] public Transform target;
    [SerializeField] private float _radianAngels;
    [SerializeField] private float _magnitude;

    private void Start()
    {
        target = Camera.main.transform;
    }

    private void Update()
    {
         RotateTo();
    }

    public void RotateTo()
    {
        Vector3 newDir = Vector3.RotateTowards(-transform.forward,
            (target.position - transform.position), _radianAngels, _magnitude);
        transform.rotation = Quaternion.LookRotation(-newDir);
    }
}
