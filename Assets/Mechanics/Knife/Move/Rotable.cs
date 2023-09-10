using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotable : MonoBehaviour, IRotable
{
    public bool Jump { get; set; }
    public float rotationSpeed = 360f; // Degrees per second
    public float rotationDecrease = 1;
    private float OldRotationSpeed;
    private void Start()
    {
        Jump = true;
        OldRotationSpeed = rotationSpeed;
    }
    public void HandleRotation()
    {
        transform.Rotate(Vector3.right * rotationSpeed * Time.deltaTime);
    }
    public void DecreaseRotation()
    {
        rotationSpeed -= rotationDecrease * Time.deltaTime;
        if (rotationSpeed < 0)
        {
            rotationSpeed = 0;
        }
    }
    public void CanRotate()
    {
        Jump = true;
        rotationSpeed = OldRotationSpeed;
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        if (Jump)
        {
            HandleRotation();
        }
        DecreaseRotation();
    }
}
