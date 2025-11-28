// File name   : PlayerController.cs
// Author(s)   : Dohun Lee, Minki Cho. Jinwon Park
// Copyright   : Copyright(C) 2025 DigiPen Institute of Technology
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Base Stats")]
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

    private bool wasGrounded;

    [Header("Squash & Stretch")]
    public Vector3 normalScale = new Vector3(1f, 1f, 1f);
    public Vector3 jumpSquashScale = new Vector3(1.2f, 0.7f, 1.2f);
    public float squashDuration = 0.1f;
    public float recoverDuration = 0.15f;

    [Header("Landing Squash Settings")]
    public Vector3 landingSquashScale = new Vector3(1.3f, 0.6f, 1.3f);
    public float landingSquashDuration = 0.1f;
    public float landingRecoverDuration = 0.15f;

    private bool landingSquashing = false;

    private bool isSquashing = false;

    [Header("Cheat / Fly Mode")]
    public bool flyCheatEnabled = false;
    public float flySpeed = 10f;
    private bool isFlying = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        distToGround = GetComponent<Collider>().bounds.extents.y;

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

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
        if (Input.GetKeyDown(KeyCode.P))
        {
            isFlying = !isFlying;
            SetFlyMode(isFlying);
        }

        if (isFlying)
            return;

        CheckGround();

        if (!wasGrounded && isGrounded)
        {
            PlayLandingEffect();
        }

        wasGrounded = isGrounded;

        Jump();

        if (!wasGrounded && isGrounded)
        {
            if (!landingSquashing)
                StartCoroutine(LandingSquash());
        }

        wasGrounded = isGrounded;
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
        float h, v;

        if (Application.isMobilePlatform && MobileInput.Instance != null)
        {
            h = MobileInput.Instance.GetHorizontal();
            v = MobileInput.Instance.GetVertical();
        }
        else
        {
            h = Input.GetAxisRaw("Horizontal");
            v = Input.GetAxisRaw("Vertical");
        }

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
        if (Input.GetKey(KeyCode.Space))
            y = 1f;
        else if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.C))
            y = -1f;

        Vector3 moveDir = new Vector3(h, y, v).normalized;

        if (moveDir.sqrMagnitude > 0.01f)
        {
            rb.linearVelocity = moveDir * flySpeed;
            Vector3 lookDir = new Vector3(moveDir.x, 0f, moveDir.z);
            if (lookDir.sqrMagnitude > 0.01f)
                transform.forward = lookDir;
        }
        else
        {
            rb.linearVelocity = Vector3.zero;
        }
    }

    void Jump()
    {
        bool jumpPressed;

        if (Application.isMobilePlatform && MobileInput.Instance != null)
        {
            jumpPressed = MobileInput.Instance.GetJump();
        }
        else
        {
            jumpPressed = Input.GetButtonDown("Jump");
        }

        if (jumpPressed && isGrounded)
        {
            rb.AddForce(Vector3.up * currentJumpForce, ForceMode.Impulse);

            if (!isSquashing)
                StartCoroutine(JumpSquash());

            if (jumpSound != null)
            {
                audioSource.volume = 0.5f;
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
            // Debug.Log("[PlayerController] LandingEffectController");
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

        float t = 0;
        while (t < squashDuration)
        {
            transform.localScale = Vector3.Lerp(normalScale, jumpSquashScale, t / squashDuration);
            t += Time.deltaTime;
            yield return null;
        }
        transform.localScale = jumpSquashScale;

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

        float t = 0f;
        while (t < landingSquashDuration)
        {
            transform.localScale = Vector3.Lerp(normalScale, landingSquashScale, t / landingSquashDuration);
            t += Time.deltaTime;
            yield return null;
        }
        transform.localScale = landingSquashScale;

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