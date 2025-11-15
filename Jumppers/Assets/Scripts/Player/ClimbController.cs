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

            // 위로 지속적으로 이동
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, climbSpeed, rb.linearVelocity.z);
        }
    }

    void TryClimbAuto()
    {
        // 캐릭터 전방으로 레이케스트를 쏘기
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
