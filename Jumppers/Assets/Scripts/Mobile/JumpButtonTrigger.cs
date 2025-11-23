using UnityEngine;
using UnityEngine.UI;

public class JumpButtonTrigger : MonoBehaviour
{
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            if (MobileInput.Instance != null)
                MobileInput.Instance.JumpButtonPressed();
        });
    }
}
