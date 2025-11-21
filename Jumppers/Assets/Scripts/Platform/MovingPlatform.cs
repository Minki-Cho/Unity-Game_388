using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Header("Movement Settings")]
    public Vector3 moveDirection = Vector3.right; // 이동 방향
    public float moveDistance = 3f;               // 왕복 거리
    public float moveSpeed = 2f;                  // 이동 속도
    public bool startAtOrigin = true;             // 시작 위치 고정 여부

    private Vector3 startPos;
    private Vector3 targetPos;
    private bool movingToTarget = true;

    void Start()
    {
        startPos = transform.position;
        targetPos = startPos + moveDirection.normalized * moveDistance;
    }

    void Update()
    {
        if (movingToTarget)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPos) < 0.05f)
                movingToTarget = false;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, startPos, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, startPos) < 0.05f)
                movingToTarget = true;
        }
    }

    // 플레이어를 플랫폼 위에 올렸을 때 자연스럽게 따라가도록
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(this.transform);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}
