using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGround : MonoBehaviour
{
    [Range(0f, 5f)]
    public float Dis2Ground;
    public LayerMask WhatIsGround;
    public bool HitGround { get; private set; }
    private MeshCollider ParentCollider;
    public CheckGround Neighbor;
    private void Start()
    {
        ParentCollider = GetComponentInParent<MeshCollider>();
    }
    private void Update()
    {

        HitGround = Physics.CheckSphere(transform.position, Dis2Ground, WhatIsGround);
        if (HitGround)
        {
            SetCollider(false);
        }
        else
        {
            SetCollider(true);
        }
    }
    void SetCollider(bool Status)
    {
        ParentCollider.isTrigger = Status;
        Neighbor.enabled = Status;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Dis2Ground);
    }
}
