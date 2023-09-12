using UnityEngine;
using System;
public class PlayerInput : MonoBehaviour
{
    public bool CanMove { get; private set; }  
    public bool CanJump { get; private set; }
    public event Action _move = delegate { };
    public event Action _jump = delegate { };
    private void Awake()
    {
        _move += GetComponent<IMoveable>().HandleMovement;
        _jump += GetComponent<IJumpable>().HandleJump;
    }
    void Update()
    {
        CanMove = Input.GetButtonDown("Jump");
        CanJump = Input.GetButtonDown("Move");
        if (CanMove)
        {
            _move();
        }
        if (CanJump)
        {
            _jump();

        }

    }
}
