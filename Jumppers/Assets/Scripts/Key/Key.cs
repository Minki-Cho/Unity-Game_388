// File name   : Key.cs
// Author(s)   : Minki Cho
// Copyright  : Copyright(C) 2025 DigiPen Institute of Technology
using UnityEngine;

public class Key : MonoBehaviour
{
    public AudioClip pickupSound;
    public ParticleSystem pickupEffect;

    private bool collected = false;

    private void OnTriggerEnter(Collider other)
    {
        if (collected) return;
        if (!other.CompareTag("Player")) return;

        collected = true;


        if (pickupSound != null)
            AudioSource.PlayClipAtPoint(pickupSound, transform.position, 1.0f);


        if (pickupEffect != null)
        {
            ParticleSystem effect = Instantiate(pickupEffect, transform.position, Quaternion.identity);
            effect.Play();
            Destroy(effect.gameObject, effect.main.duration + 0.5f);
        }

       GameManager.Instance.GameClear();

        Destroy(gameObject);
    }
}
