using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Transform target;

    [Header("Offset & Damping")]
    [SerializeField] private Vector3 offset = new Vector3(0, 3, -8);
    [SerializeField] private float followSpeed = 5f;
    [SerializeField] private float yLeadAmount = 2f;   // ���� �� ���� �̸� ����
    [SerializeField] private float yLeadSpeed = 2f;    // ���� ��ȯ �ӵ�

    private float currentYLead = 0f;
    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        if (target == null) return;

        // �÷��̾� ���̿� ���� Y ���� ���
        float verticalVelocity = target.GetComponent<Rigidbody>()?.linearVelocity.y ?? 0f;
        float targetYLead = Mathf.Lerp(currentYLead, verticalVelocity > 0 ? yLeadAmount : -yLeadAmount, Time.deltaTime * yLeadSpeed);
        currentYLead = Mathf.Clamp(targetYLead, -yLeadAmount, yLeadAmount);

        // ��ǥ ��ġ = Ÿ�� + ������ + Y ����
        Vector3 desiredPosition = target.position + offset + new Vector3(0, currentYLead, 0);

        // �ε巴�� �������
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, 1f / followSpeed);

        // �׻� Ÿ�� �ٶ󺸱�
        transform.LookAt(target.position + Vector3.up * 1.5f);
    }
}
