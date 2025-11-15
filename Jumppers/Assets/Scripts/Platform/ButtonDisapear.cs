using UnityEngine;

public class ButtonDisappear : MonoBehaviour
{
    public float delay = 0.3f; // 사라지는 딜레이

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.attachedRigidbody != null)
        {
            // 플레이어의 발 위치가 버튼보다 위인지 체크
            if (other.transform.position.y > transform.position.y + 0.1f)
            {
                // 0.3초 뒤에 사라짐
                Invoke(nameof(Disappear), delay);
            }
        }
    }

    void Disappear()
    {
        gameObject.SetActive(false);
    }
}
