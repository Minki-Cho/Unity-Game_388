using UnityEngine;

public class KillZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // 1. 트리거에 닿은 것이 플레이어인지 태그로 확인
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered Kill Zone.");
            // 2. 플레이어 스크립트의 "Die" 함수를 호출
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.Die();
            }
        }
    }
}
