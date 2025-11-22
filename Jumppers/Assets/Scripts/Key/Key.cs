using UnityEngine;

public class Key : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // 진원 박은 여기다가 연결 시키면 된다다다다.........
            //GameManager.Instance.GameClear();

            // 열쇠 제거
            Destroy(gameObject);
        }
    }
}
