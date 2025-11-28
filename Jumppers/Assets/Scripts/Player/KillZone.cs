// File name   : KillZone.cs
// Author(s)   : Dohun Lee
// Copyright   : Copyright(C) 2025 DigiPen Institute of Technology
using UnityEngine;

public class KillZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered Kill Zone.");
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.Die();
            }
        }
    }
}
