using UnityEngine;

public class StatModifierPlatform : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("1이면 정상 속도, 0.5면 반토막(느려짐), 2면 2배(빨라짐)")]
    public float speedMultiplier = 1f;

    [Tooltip("1이면 정상 점프, 2면 슈퍼 점프")]
    public float jumpMultiplier = 1f;

    private void OnCollisionEnter(Collision collision)
    {
        // 플레이어가 닿았을 때
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                // 능력치 변경 적용
                player.currentMoveSpeed = player.defaultMoveSpeed * speedMultiplier;
                player.currentJumpForce = player.defaultJumpForce * jumpMultiplier;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // 플레이어가 발판에서 나갔을 때 (점프하거나 걸어나감)
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                // 능력치 원상복구
                player.ResetStats();
            }
        }
    }
}
