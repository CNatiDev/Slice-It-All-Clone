using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moveable : MonoBehaviour,IMoveable
{
    public float _forwardForce = 0.0f;
    private Rigidbody _rb;
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }
    public void HandleMovement()
    {
        _rb.AddForce(Vector3.forward * _forwardForce, ForceMode.VelocityChange);
    }
}
