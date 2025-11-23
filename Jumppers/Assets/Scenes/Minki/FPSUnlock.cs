using UnityEngine;

public class FPSUnlock : MonoBehaviour
{
    void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 120;
    }
}