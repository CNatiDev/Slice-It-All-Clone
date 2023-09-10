using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moveable : MonoBehaviour,IMoveable
{
    public float ForwardForce = 0.0f;
    private Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void HandleMovement()
    {
        rb.AddForce(Vector3.forward * ForwardForce, ForceMode.VelocityChange);
    }
}
