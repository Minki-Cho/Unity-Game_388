using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

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

    // --- Squash & Stretch 설정 ---
    [Header("Squash & Stretch")]
    public Vector3 normalScale = new Vector3(1f, 1f, 1f);
    public Vector3 jumpSquashScale = new Vector3(1.2f, 0.7f, 1.2f);
    public float squashDuration = 0.1f;   // 찌그러지는 속도
    public float recoverDuration = 0.15f; // 원상복구 속도

    [Header("Landing Squash Settings")]
    public Vector3 landingSquashScale = new Vector3(1.3f, 0.6f, 1.3f);
    public float landingSquashDuration = 0.1f;
    public float landingRecoverDuration = 0.15f;

    private bool landingSquashing = false;

    private bool isSquashing = false;

    // --------- ✈ 치트 플라잉 모드 ---------
    [Header("Cheat / Fly Mode")]
    public bool flyCheatEnabled = false;   // 인스펙터에서 켜두면 시작부터 날기 가능
    public float flySpeed = 10f;
    private bool isFlying = false;
    // -----------------------------------

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
        // ✈ P 키로 플라잉 모드 토글
        if (Input.GetKeyDown(KeyCode.P))
        {
            isFlying = !isFlying;
            SetFlyMode(isFlying);
        }

        // 플라잉 모드일 땐 땅 체크/점프/스쿼시 로직 스킵
        if (isFlying)
            return;

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

        //if (!wasGrounded && isGrounded)
        //{
        //    if (!landingSquashing)
        //        StartCoroutine(LandingSquash());
        //}

        wasGrounded = isGrounded; // 상태 갱신
    }

    void FixedUpdate()
    {
        if (isFlying)
        {
            FlyMove();
        }
        else
        {
            Move();
            ApplyJumpPhysics();
        }
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

    void FlyMove()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        float y = 0f;
        if (Input.GetKey(KeyCode.Space))          // 위로
            y = 1f;
        else if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.C)) // 아래로
            y = -1f;

        Vector3 moveDir = new Vector3(h, y, v).normalized;

        if (moveDir.sqrMagnitude > 0.01f)
        {
            rb.linearVelocity = moveDir * flySpeed;
            // 이동 방향으로 바라보게 (위/아래는 무시)
            Vector3 lookDir = new Vector3(moveDir.x, 0f, moveDir.z);
            if (lookDir.sqrMagnitude > 0.01f)
                transform.forward = lookDir;
        }
        else
        {
            // 멈춰 있을 땐 관성 제거
            rb.linearVelocity = Vector3.zero;
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * currentJumpForce, ForceMode.Impulse);

            if (!isSquashing)
                StartCoroutine(JumpSquash());

            // 🔊 점프 사운드
            if (jumpSound != null)
            {
                audioSource.volume = 0.5f;  // 절반 볼륨
                audioSource.PlayOneShot(jumpSound);
            }

        }
    }

    private void SetFlyMode(bool enable)
    {
        if (rb == null) return;

        if (enable)
        {
            rb.useGravity = false;
            rb.linearVelocity = Vector3.zero;
            transform.localScale = normalScale;
        }
        else
        {
            rb.useGravity = true;
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
        //transform.position = spawnPoint.position;
        //transform.rotation = spawnPoint.rotation;

        //if (rb == null) rb = GetComponent<Rigidbody>();
        //rb.linearVelocity = Vector3.zero;
        //rb.angularVelocity = Vector3.zero;

        //this.enabled = true;
        SceneManager.LoadScene("Game");
    }

    IEnumerator JumpSquash()
    {
        isSquashing = true;

        // 1) 아래로 찌그러트리기
        float t = 0;
        while (t < squashDuration)
        {
            transform.localScale = Vector3.Lerp(normalScale, jumpSquashScale, t / squashDuration);
            t += Time.deltaTime;
            yield return null;
        }
        transform.localScale = jumpSquashScale;

        // 2) 다시 원래 사이즈로 천천히 복구
        t = 0;
        while (t < recoverDuration)
        {
            transform.localScale = Vector3.Lerp(jumpSquashScale, normalScale, t / recoverDuration);
            t += Time.deltaTime;
            yield return null;
        }
        transform.localScale = normalScale;

        isSquashing = false;
    }

    IEnumerator LandingSquash()
    {
        landingSquashing = true;

        // 1) 착지 → 아래로 찌그러짐
        float t = 0f;
        while (t < landingSquashDuration)
        {
            transform.localScale = Vector3.Lerp(normalScale, landingSquashScale, t / landingSquashDuration);
            t += Time.deltaTime;
            yield return null;
        }
        transform.localScale = landingSquashScale;

        // 2) 다시 원래로 복구
        t = 0f;
        while (t < landingRecoverDuration)
        {
            transform.localScale = Vector3.Lerp(landingSquashScale, normalScale, t / landingRecoverDuration);
            t += Time.deltaTime;
            yield return null;
        }
        transform.localScale = normalScale;

        landingSquashing = false;
    }
}