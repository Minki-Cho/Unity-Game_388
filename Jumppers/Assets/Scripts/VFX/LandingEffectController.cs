using UnityEngine;

public class LandingEffectController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform landingPoint;       // 발밑 위치
    [SerializeField] private ParticleSystem landingDust;  // 씬에 있는 파티클

    public void PlayLandingEffect()
    {
        if (landingDust == null || landingPoint == null)
        {
            Debug.LogWarning("[LandingEffectController] landingDust or landingPoint is not set!");
            return;
        }

        // 발밑 위치로 옮기고
        Transform dustTransform = landingDust.transform;
        dustTransform.position = landingPoint.position;

        // 각도도 Player 방향에 맞추고 싶으면 (선택)
        // dustTransform.forward = Vector3.up; // 방향 고정하거나, 필요에 맞게 설정

        // 파티클 재생
        landingDust.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        landingDust.Play();
    }
}