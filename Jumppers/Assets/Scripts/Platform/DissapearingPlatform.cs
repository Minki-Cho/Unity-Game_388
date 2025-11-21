using UnityEngine;
using System.Collections;

public class DisappearingPlatform : MonoBehaviour
{
    [Header("Timings")]
    public float initialDelay = 3f;       // 처음 3초 평범하게 유지
    public float blinkDuration = 3f;      // 깜박이는 시간
    public float blinkInterval = 0.2f;    // 깜박 속도
    public float fallSpeed = 2f;          // 아래로 떨어지는 속도
    public float respawnTime = 5f;        // 다시 나타나는 시간 (0이면 영구 소멸)

    private Renderer[] renderers;
    private Collider col;
    private Vector3 originalPosition;
    private bool triggered = false;

    void Start()
    {
        renderers = GetComponentsInChildren<Renderer>();
        col = GetComponent<Collider>();
        originalPosition = transform.position;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!triggered && collision.gameObject.CompareTag("Player"))
        {
            triggered = true;
            StartCoroutine(DisappearRoutine());
        }
    }

    IEnumerator DisappearRoutine()
    {
        // ------------------------------
        // 🟢 1) 처음 3초 동안 아무 변화 없음
        // ------------------------------
        yield return new WaitForSeconds(initialDelay);

        // ------------------------------
        // 🟡 2) 깜박거리기 (blinkDuration 동안)
        // ------------------------------
        float timer = 0f;
        while (timer < blinkDuration)
        {
            ToggleRenderers(false);
            yield return new WaitForSeconds(blinkInterval);

            ToggleRenderers(true);
            yield return new WaitForSeconds(blinkInterval);

            timer += blinkInterval * 2;
        }

        // ------------------------------
        // 🔴 3) 아래로 떨어지면서 사라짐
        // ------------------------------
        col.enabled = false; // 플랫폼 충돌 비활성화

        float fallTimer = 0f;
        float fallTime = 1f; // 1초 동안 떨어짐

        while (fallTimer < fallTime)
        {
            transform.position += Vector3.down * fallSpeed * Time.deltaTime;
            fallTimer += Time.deltaTime;
            yield return null;
        }

        // 완전히 숨김
        ToggleRenderers(false);

        // ------------------------------
        // ♻️ 4) respawnTime 후 다시 등장 (옵션)
        // ------------------------------
        if (respawnTime > 0)
        {
            yield return new WaitForSeconds(respawnTime);

            // 위치 원상복구
            transform.position = originalPosition;

            ToggleRenderers(true);
            col.enabled = true;
            triggered = false;
        }
    }

    void ToggleRenderers(bool state)
    {
        foreach (var r in renderers)
            r.enabled = state;
    }
}
