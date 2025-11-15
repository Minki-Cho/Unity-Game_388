using UnityEngine;

public class ButtonPlatformActivator : MonoBehaviour
{
    public GameObject platformToActivate; // 나타날 플랫폼
    public float delay = 0.3f;            // 버튼 밟은 후 딜레이

    private bool activated = false;

    private void OnTriggerEnter(Collider other)
    {
        if (activated) return;

        if (other.CompareTag("Player") || other.attachedRigidbody != null)
        {
            // 위에서 밟았는지 체크
            if (other.transform.position.y > transform.position.y + 0.1f)
            {
                activated = true;
                Invoke(nameof(ActivatePlatform), delay);
            }
        }
    }

    private void ActivatePlatform()
    {
        if (platformToActivate != null)
            platformToActivate.SetActive(true);

        // 버튼 사라지기
        gameObject.SetActive(false);
    }
}
