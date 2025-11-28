//File name    : CameraFollow.cs
//Author(s)    : Jin Park
//Copyright    : Copyright (C) 2025 DigiPen Institute of Technology 

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


    private float currentYLead = 0f;
    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        if (target == null) return;

        Rigidbody rb = target.GetComponent<Rigidbody>();
        float verticalVelocity = 0f;
        if (rb != null)
            verticalVelocity = rb.linearVelocity.y;

        float targetYLead = Mathf.Lerp(currentYLead,
            verticalVelocity > 0 ? yLeadAmount : -yLeadAmount,
            Time.deltaTime * yLeadSpeed);
        currentYLead = Mathf.Clamp(targetYLead, -yLeadAmount, yLeadAmount);

        Vector3 desiredPosition = target.position
                                  + offset
                                  + new Vector3(0, currentYLead, 0);

        float distanceToDesired = Vector3.Distance(transform.position, desiredPosition);

        if (distanceToDesired <= deadZoneRadius)
        {
            transform.LookAt(target.position + Vector3.up * 1.5f);
            return;
        }

        transform.position = Vector3.SmoothDamp(transform.position,
            desiredPosition,
            ref velocity,
            1f / followSpeed);

        transform.LookAt(target.position + Vector3.up * 1.5f);
    }

}
