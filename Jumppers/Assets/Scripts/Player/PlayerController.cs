using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Base Stats (기본 능력치)")]
    public float defaultMoveSpeed = 6f;
    public float defaultJumpForce = 7f;

    // 실제 게임 로직에서 사용할 변수 (외부에서 접근 가능하도록 public이지만, 에디터에선 안 보이게)
    [HideInInspector] public float currentMoveSpeed;
    [HideInInspector] public float currentJumpForce;

    public float fallMultiplier = 2.5f;

    public LayerMask groundLayer; // 바닥으로 인식할 레이어

    private Rigidbody rb;
    private bool isGrounded;
    private bool isOnPlatform;
    private float distToGround;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // 플레이어의 콜라이더 중심에서 바닥까지의 거리 + 약간의 여유값
        distToGround = GetComponent<Collider>().bounds.extents.y;
        isOnPlatform = false;
        ResetStats();
        Debug.Log("Distance to ground: " + distToGround);
    }

    public void ResetStats()
    {
        currentMoveSpeed = defaultMoveSpeed;
        currentJumpForce = defaultJumpForce;
    }

    void Update()
    {
        CheckGround();
        Jump();
    }

    void FixedUpdate()
    {
        Move();
        ApplyJumpPhysics();
    }

    void Move()
    {
        float h = Input.GetAxisRaw("Horizontal"); // A, D
        float v = Input.GetAxisRaw("Vertical");   // W, S

        // 이동 방향 벡터 생성 (Y축은 배제)
        Vector3 moveDir = (Vector3.right * h + Vector3.forward * v).normalized;

        // 입력이 있을 때만 이동
        if (moveDir.magnitude >= 0.1f)
        {
            // Y축 속도(중력)는 유지하고 X, Z 속도만 변경
            Vector3 targetVelocity = moveDir * currentMoveSpeed;
            rb.linearVelocity = new Vector3(targetVelocity.x, rb.linearVelocity.y, targetVelocity.z);

            // 캐릭터가 이동 방향을 바라보게 회전 (선택 사항)
            transform.forward = moveDir;
        }
        else
        {
            // 입력 없으면 미끄러짐 방지를 위해 X, Z 멈춤
            rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump")&& isGrounded)
        {
            // 순간적인 힘을 위로 가함
            rb.AddForce(Vector3.up * currentJumpForce, ForceMode.Impulse);
        }
    }

    void ApplyJumpPhysics()
    {
        // 1. 낙하 가속 (Fast Fall)
        // velocity.y < 0 은 플레이어가 떨어지고 있다는 의미
        if (rb.linearVelocity.y < 0)
        {
            // (fallMultiplier - 1)을 하는 이유:
            // 유니티가 기본 중력(1)을 이미 적용 중이므로, 우리는 '추가할' 중력만 더해줌
            rb.linearVelocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        }
    }

    void CheckGround()
    {
        // 플레이어 중심에서 아래로 레이저를 쏴서 바닥이 있는지 확인
        // 0.1f는 여유 거리
        isGrounded = Physics.Raycast(transform.position, Vector3.down, distToGround + 0.5f, groundLayer);

        // 디버깅용 레이저 그리기 (Scene 뷰에서만 보임)
        Debug.DrawRay(transform.position, Vector3.down * (distToGround + 0.1f), Color.red);
    }

    public void Die()
    {
        this.enabled = false;

        GameManager.Instance.HandlePlayerDeath();
    }

    public void Respawn(Transform spawnPoint)
    {
        // 1. 위치 리셋
        transform.position = spawnPoint.position;
        transform.rotation = spawnPoint.rotation;

        // 2. (중요!) 속도 초기화 - 이걸 안 하면 추락 속도가 남아서 바닥을 뚫고 감
        if (rb == null) rb = GetComponent<Rigidbody>();
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero; // 회전 속도도 0으로

        // 3. 다시 스크립트를 활성화해서 조작 가능하게 함
        this.enabled = true;

        Debug.Log("Player Respawned!");
    }
}
