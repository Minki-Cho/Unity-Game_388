// File name   : MobileUVVisibillity.cs
// Author(s)   : Minki Cho
// Copyright   : Copyright(C) 2025 DigiPen Institute of Technology
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
