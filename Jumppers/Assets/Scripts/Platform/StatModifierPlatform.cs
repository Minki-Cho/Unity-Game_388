// File name   : StartModifierPlatform.cs
// Author(s)   : Dohun Lee, Minki Cho
// Copyright   : Copyright(C) 2025 DigiPen Institute of Technology
using UnityEngine;
using UnityEngine;

public class StatModifierPlatform : MonoBehaviour
{
    [Header("Settings")]
    public float speedMultiplier = 1f;

    public float jumpMultiplier = 1f;

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {

                player.currentMoveSpeed = player.defaultMoveSpeed * speedMultiplier;
                player.currentJumpForce = player.defaultJumpForce * jumpMultiplier;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {

                player.ResetStats();
            }
        }
    }
}
