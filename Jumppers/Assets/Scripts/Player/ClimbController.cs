// File name   : ClimbController.cs
// Author(s)   : Minki Cho
// Copyright   : Copyright(C) 2025 DigiPen Institute of Technology
using UnityEngine;

public class WallClimbAuto : MonoBehaviour
{
    public Rigidbody rb;
    public float climbSpeed = 3f;
    public float wallCheckDistance = 0.6f;
    public LayerMask wallLayer;

    private bool isClimbing = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        TryClimbAuto();
    }

    void FixedUpdate()
    {
        if (isClimbing)
        {
            rb.useGravity = false;

            rb.linearVelocity = new Vector3(rb.linearVelocity.x, climbSpeed, rb.linearVelocity.z);
        }
    }

    void TryClimbAuto()
    {

        if (Physics.Raycast(transform.position, transform.forward, wallCheckDistance, wallLayer))
        {
            StartClimb();
        }
        else
        {
            StopClimb();
        }
    }

    void StartClimb()
    {
        if (!isClimbing)
        {
            isClimbing = true;
            rb.useGravity = false;
        }
    }

    void StopClimb()
    {
        if (isClimbing)
        {
            isClimbing = false;
            rb.useGravity = true;
        }
    }
}
