using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumpable : MonoBehaviour, IJumpable
{
    [SerializeField] private float _jumpForce = 5f;
    private Rigidbody _rb;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }
    private void Awake()
    {
        GetComponent<PlayerInput>()._jump += HandleJump;
    }
    public void HandleJump()
    {
        // Add an upward force to the knife
        _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        _rb.constraints &= ~RigidbodyConstraints.FreezeRotationX;
        GetComponent<IRotable>().CanRotate();
    }
    private void OnCollisionEnter(Collision collision)
    {
        GetComponent<IRotable>()._jump = false;
        _rb.constraints |= RigidbodyConstraints.FreezeRotationX;
        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
    }

}
