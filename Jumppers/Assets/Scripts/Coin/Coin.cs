using UnityEngine;

public class Coin : MonoBehaviour
{
    public AudioClip coin_pickupSound;         // 코인 먹는 소리
    public ParticleSystem coin_pickupEffect;   // 파티클 효과
    private bool collected = false;

    public int value = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (collected) return;
        if (!other.CompareTag("Player")) return;

        collected = true;

        // 1. 코인 점수 증가
        GameManager.Instance.AddCoin(value);

        // 2. 사운드 재생
        if (coin_pickupSound != null)
            AudioSource.PlayClipAtPoint(coin_pickupSound, transform.position, 1.0f);

        // 3. 파티클 생성 후 재생
        if (coin_pickupEffect != null)
        {
            ParticleSystem effect = Instantiate(coin_pickupEffect, transform.position, Quaternion.identity);
            effect.Play();
            Destroy(effect.gameObject, effect.main.duration + 0.5f);
        }

        // 4. 코인 오브젝트 삭제
        Destroy(gameObject);
    }
}
