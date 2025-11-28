// File name   : CoinRotate.cs
// Author(s)   : Minki Cho
// Copyright  : Copyright(C) 2025 DigiPen Institute of Technology
using UnityEngine;

public class CoinRotate : MonoBehaviour
{
    public float rotateSpeed = 180f;

    void Update()
    {
        transform.Rotate(0f, rotateSpeed * Time.deltaTime, 0f);
    }
}