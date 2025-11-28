//File name    : FPSOptionsUI.cs
//Author(s)    : Jin Park
//Copyright    : Copyright (C) 2025 DigiPen Institute of Technology 

using UnityEngine;
using UnityEngine.UI;

public class FPSOptionsUI : MonoBehaviour
{
    [SerializeField] private Toggle fpsToggle;

    private void Start()
    {
        if (fpsToggle == null)
        {
            return;
        }

        if (FPSDisplay.Instance != null)
        {
            fpsToggle.isOn = FPSDisplay.Instance.IsShowing;
        }
    }

    public void OnFPSToggleChanged(bool value)
    {
        if (FPSDisplay.Instance != null)
        {
            FPSDisplay.Instance.SetShowFPSFromUI(value);
        }
    }
}