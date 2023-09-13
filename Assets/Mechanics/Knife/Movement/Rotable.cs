using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotable : MonoBehaviour, IRotable
{   
    public bool _jump { get; set; }
    public float _rotationSpeed = 360f; // Degrees per second
    public float _rotationDecrease = 1;
    private float _oldRotationSpeed;
    private bool isIncreasing = true;
    private void Start()
    {
        _jump = false;
        _oldRotationSpeed = _rotationSpeed;
    }
    public void HandleRotation()
    {
        transform.Rotate(Vector3.right * _rotationSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Call when knife is not grounded
    /// </summary>
    private void HandleRotationSpeed()
    {
        if (isIncreasing)
        {
            _rotationSpeed += _rotationDecrease * Time.deltaTime;
            if (_rotationSpeed >= _oldRotationSpeed)
            {
                _rotationSpeed = _oldRotationSpeed;
                isIncreasing = false;
            }
        }
        else
        {
            _rotationSpeed -= _rotationDecrease * Time.deltaTime;
            if (_rotationSpeed <= 0)
            {
                _rotationSpeed = 0;
                isIncreasing = true;
            }
        }
        if (!_jump)
        {
            _rotationSpeed = 0;
        }
    }
    public void CanRotate()
    {
        _jump = true;
        _rotationSpeed = _oldRotationSpeed;
    }
    private void FixedUpdate()
    {
        if (_jump)
        {
            HandleRotation();
        }
        HandleRotationSpeed();
    }
}
