using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotable : MonoBehaviour, IRotable
{
    public bool Jump { get; set; }
    public float rotationSpeed = 360f; // Degrees per second
    public float rotationDecrease = 1;
    private float OldRotationSpeed;
    private bool isIncreasing = true;
    private void Start()
    {
        Jump = false;
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
    public void IncreaseRotation()
    {
        rotationSpeed += rotationDecrease * Time.deltaTime;
        if (rotationSpeed > OldRotationSpeed)
        {
            rotationSpeed = OldRotationSpeed;
        }

    }
    private void HandleRotationSpeed()
    {
        if (isIncreasing)
        {
            rotationSpeed += rotationDecrease * Time.deltaTime;
            if (rotationSpeed >= OldRotationSpeed)
            {
                rotationSpeed = OldRotationSpeed;
                isIncreasing = false;
            }
        }
        else
        {
            rotationSpeed -= rotationDecrease * Time.deltaTime;
            if (rotationSpeed <= 0)
            {
                rotationSpeed = 0;
                isIncreasing = true;
            }
        }
        if (!Jump)
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
        HandleRotationSpeed();
    }
}
