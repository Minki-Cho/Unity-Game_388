using UnityEngine;

public class LandingEffectController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform landingPoint;     
    [SerializeField] private GameObject landingDustPrefab; 

    public void PlayLandingEffect()
    {
        if (landingDustPrefab == null || landingPoint == null)
        {
            Debug.LogWarning("[LandingEffectController] Prefab or landingPoint is not set!");
            return;
        }

        GameObject dust = Instantiate(
            landingDustPrefab,
            landingPoint.position,
            Quaternion.identity
        );


        ParticleSystem ps = dust.GetComponent<ParticleSystem>();
        if (ps != null)
        {
            Destroy(dust, ps.main.duration + ps.main.startLifetimeMultiplier);
        }
        else
        {
            Destroy(dust, 2f); // ¹é¾÷¿ë
        }
    }
}