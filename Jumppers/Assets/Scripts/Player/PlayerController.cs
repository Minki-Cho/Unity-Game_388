using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Base Stats (기본 능력치)")]
    public float defaultMoveSpeed = 6f;
    public float defaultJumpForce = 7f;

    [HideInInspector] public float currentMoveSpeed;
    [HideInInspector] public float currentJumpForce;

    public float fallMultiplier = 2.5f;
    public LayerMask groundLayer;

    private Rigidbody rb;
    private bool isGrounded;
    private float distToGround;

    [Header("Audio Settings")]
    public AudioClip jumpSound;
    private AudioSource audioSource;

    [Header("VFX")]
    [SerializeField] private LandingEffectController landingEffectController;

    // 착지 순간 감지용
    private bool wasGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        distToGround = GetComponent<Collider>().bounds.extents.y;

        // 오디오 초기화
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        // VFX 컨트롤러 자동 할당 (같은 오브젝트에 붙어 있다면)
        if (landingEffectController == null)
            landingEffectController = GetComponent<LandingEffectController>();

        ResetStats();
    }

    public void ResetStats()
    {
        currentMoveSpeed = defaultMoveSpeed;
        currentJumpForce = defaultJumpForce;
    }

    void Update()
    {
        // 1) 먼저 바닥 체크
        CheckGround();

        // 2) 공중(false)이었다가 이번 프레임에 땅(true)이 되면 = 착지
        if (!wasGrounded && isGrounded)
        {
            PlayLandingEffect();
        }

        // 3) 착지 체크 후, 이번 프레임 상태를 저장
        wasGrounded = isGrounded;

        // 4) 점프는 원래대로 유지
        Jump();
    }

    void FixedUpdate()
    {
        Move();
        ApplyJumpPhysics();
    }

    void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 moveDir = (Vector3.right * h + Vector3.forward * v).normalized;

        if (moveDir.magnitude >= 0.1f)
        {
            Vector3 targetVelocity = moveDir * currentMoveSpeed;
            rb.linearVelocity = new Vector3(targetVelocity.x, rb.linearVelocity.y, targetVelocity.z);

            transform.forward = moveDir;
        }
        else
        {
            rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * currentJumpForce, ForceMode.Impulse);

            // 🔊 점프 사운드
            if (jumpSound != null)
                audioSource.PlayOneShot(jumpSound);
        }
    }

    void ApplyJumpPhysics()
    {
        if (rb.linearVelocity.y < 0)
        {
            rb.linearVelocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        }
    }

    void CheckGround()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, distToGround + 0.5f, groundLayer);
        Debug.DrawRay(transform.position, Vector3.down * (distToGround + 0.1f), Color.red);
    }

    private void PlayLandingEffect()
    {
        if (landingEffectController != null)
        {
            landingEffectController.PlayLandingEffect();
        }
        else
        {
            // 디버그용
            // Debug.Log("[PlayerController] LandingEffectController가 할당되지 않음");
        }
    }

    public void Die()
    {
        this.enabled = false;
        GameManager.Instance.GameOver();
    }

    public void Respawn(Transform spawnPoint)
    {
        transform.position = spawnPoint.position;
        transform.rotation = spawnPoint.rotation;

        if (rb == null) rb = GetComponent<Rigidbody>();
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        this.enabled = true;
    }
}
