// File name   : FPSUnlock
// Author(s)   : Minki Cho
// Copyright   : Copyright(C) 2025 DigiPen Institute of Technology
using UnityEngine;

public class FPSUnlock : MonoBehaviour
{
    void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 120;
    }
}