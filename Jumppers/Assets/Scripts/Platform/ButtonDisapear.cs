using UnityEngine;

public class ButtonPlatformActivator : MonoBehaviour
{
    public GameObject platformToActivate;
    public float delay = 0.3f;

    private bool activated = false;

    private void OnTriggerEnter(Collider other)
    {
        if (activated) return;

        if (other.CompareTag("Player") || other.attachedRigidbody != null)
        {
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

        gameObject.SetActive(false);
    }
}
