using UnityEngine;

public class Coin : MonoBehaviour
{
    public AudioClip coin_pickupSound;
    public ParticleSystem coin_pickupEffect;
    private bool collected = false;

    public int value = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (collected) return;
        if (!other.CompareTag("Player")) return;

        collected = true;

        GameManager.Instance.AddCoin(value);

        if (coin_pickupSound != null)
            AudioSource.PlayClipAtPoint(coin_pickupSound, transform.position, 1.0f);

        if (coin_pickupEffect != null)
        {
            ParticleSystem effect = Instantiate(coin_pickupEffect, transform.position, Quaternion.identity);
            effect.Play();
            Destroy(effect.gameObject, effect.main.duration + 0.5f);
        }

        Destroy(gameObject);
    }
}
