// File name   : Coin.cs
// Author(s)   : Minki Cho
// Copyright  : Copyright(C) 2025 DigiPen Institute of Technology
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MobileInput : MonoBehaviour
{
    public void Test()
    {
        Debug.Log("test");
    }
    public static MobileInput Instance;

    public Joystick joystick;
    private bool jumpPressed = false;

    private void Awake()
    {
        Instance = this;
    }

    public float GetHorizontal() => joystick != null ? joystick.Horizontal() : 0f;
    public float GetVertical() => joystick != null ? joystick.Vertical() : 0f;

    public bool GetJump()
    {
        bool temp = jumpPressed;
        jumpPressed = false;
        return temp;
    }

    public void JumpButtonPressed()
    {
        jumpPressed = true;
    }
}
