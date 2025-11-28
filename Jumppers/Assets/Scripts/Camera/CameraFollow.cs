using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Transform target;

    [Header("Offset & Damping")]
    [SerializeField] private Vector3 offset = new Vector3(0, 3, -8);
    [SerializeField] private float followSpeed = 5f;
    [SerializeField] private float yLeadAmount = 2f;
    [SerializeField] private float yLeadSpeed = 2f;

    [Header("Dead Zone")]
    [Tooltip("The distance allowed before the camera moves")]
    [SerializeField] private float deadZoneRadius = 1.5f;

    [Header("Vertical Speed Tuning")]
    [Tooltip("Expected max jump speed (for normalization)")]
    [SerializeField] private float maxJumpSpeed = 15f;

    private float currentYLead = 0f;
    private Vector3 velocity = Vector3.zero;
    private Rigidbody targetRb;

    private void Awake()
    {
        if (target != null)
        {
            targetRb = target.GetComponent<Rigidbody>();
        }
    }

    void LateUpdate()
    {
        if (target == null) return;

        float verticalVelocity = 0f;
        if (targetRb != null)
            verticalVelocity = targetRb.linearVelocity.y;   

        float normalizedVy = 0f;
        if (maxJumpSpeed > 0f)
            normalizedVy = Mathf.Clamp(verticalVelocity / maxJumpSpeed, -1f, 1f);

        float targetYLead = normalizedVy * yLeadAmount;

        currentYLead = Mathf.Lerp(currentYLead, targetYLead, Time.deltaTime * yLeadSpeed);

        Vector3 desiredPosition = target.position
                                  + offset
                                  + new Vector3(0, currentYLead, 0);

        float distanceToDesired = Vector3.Distance(transform.position, desiredPosition);

        if (distanceToDesired > deadZoneRadius)
        {
            transform.position = Vector3.SmoothDamp(transform.position,
                desiredPosition,
                ref velocity,
                1f / followSpeed);
        }

        transform.LookAt(target.position + Vector3.up * 1.5f);
    }
}
