// File name   : JumpButtonTrigger.cs
// Author(s)   : Minki Cho
// Copyright  : Copyright(C) 2025 DigiPen Institute of Technology
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
