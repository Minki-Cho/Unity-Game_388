using UnityEngine;

public class Key : MonoBehaviour
{
    public AudioClip pickupSound;      // 먹는 소리
    public ParticleSystem pickupEffect; // 파티클

    private bool collected = false;

    private void OnTriggerEnter(Collider other)
    {
        if (collected) return;
        if (!other.CompareTag("Player")) return;

        collected = true;


        if (pickupSound != null)
            AudioSource.PlayClipAtPoint(pickupSound, transform.position, 1.0f);

        // 2. 파티클 재생
        if (pickupEffect != null)
        {
            // 파티클은 Key가 사라져도 재생되게 부모 분리 후 실행
            ParticleSystem effect = Instantiate(pickupEffect, transform.position, Quaternion.identity);
            effect.Play();
            Destroy(effect.gameObject, effect.main.duration + 0.5f);
        }

        // 진원박은 여기다가 만들면 된다다다다다다다다다.
       // GameManager.Instance.GameClear();

        // 4. 키 오브젝트 삭제
        Destroy(gameObject);
    }
}
