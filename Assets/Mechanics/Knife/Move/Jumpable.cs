using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumpable : MonoBehaviour, IJumpable
{
    [SerializeField] private float jumpForce = 5f;
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Awake()
    {
        GetComponent<PlayerInput>().Jump += HandleJump;
    }
    public void HandleJump()
    {
        // Add an upward force to the knife
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        rb.constraints &= ~RigidbodyConstraints.FreezeRotationX;
        GetComponent<IRotable>().CanRotate();
    }
    private void OnCollisionEnter(Collision collision)
    {
        GetComponent<IRotable>().Jump = false;
        rb.constraints |= RigidbodyConstraints.FreezeRotationX;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

}
