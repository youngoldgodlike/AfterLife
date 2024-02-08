using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTowardsFor : MonoBehaviour
{
    [SerializeField] private Transform target;
    
    private void Start()
    {
        if (Camera.main != null) target = Camera.main.transform;
    }

    private void Update()
    {
        RotateTo();
    }

    public void RotateTo()
    {
        Vector3 newDir = Vector3.RotateTowards(-transform.forward, 
            (target.position - transform.position), 1f, 0f);
        transform.rotation = Quaternion.LookRotation(-newDir);
    }
}
