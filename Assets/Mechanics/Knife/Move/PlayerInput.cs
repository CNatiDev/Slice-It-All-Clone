using UnityEngine;
using System;
public class PlayerInput : MonoBehaviour
{
    public bool CanMove { get; private set; }  
    public bool CanJump { get; private set; }
    public event Action Move = delegate { };
    public event Action Jump = delegate { };
    private void Awake()
    {
        Move += GetComponent<IMoveable>().HandleMovement;
        Jump += GetComponent<IJumpable>().HandleJump;
    }
    void Update()
    {
        CanMove = Input.GetButtonDown("Jump");
        CanJump = Input.GetButtonDown("Move");
        if (CanMove)
        {
            Move();
        }
        if (CanJump)
        {
            Jump();

        }

    }
}
