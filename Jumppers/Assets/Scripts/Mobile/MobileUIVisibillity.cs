using UnityEngine;

public class MobileUIVisibility : MonoBehaviour
{
    void Awake()
    {
        bool mobile =
            Application.isMobilePlatform ||
            Application.platform == RuntimePlatform.Android ||
            Application.platform == RuntimePlatform.IPhonePlayer;

        gameObject.SetActive(mobile);
    }
}
