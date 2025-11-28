//File name    : LandingEffectController.cs
//Author(s)    : Jin Park
//Copyright    : Copyright (C) 2025 DigiPen Institute of Technology 

using UnityEngine;

public class LandingEffectController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform landingPoint;
    [SerializeField] private ParticleSystem landingDust;

    public void PlayLandingEffect()
    {
        if (landingDust == null || landingPoint == null)
        {
            Debug.LogWarning("[LandingEffectController] landingDust or landingPoint is not set!");
            return;
        }

        Transform dustTransform = landingDust.transform;
        dustTransform.position = landingPoint.position;

        landingDust.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        landingDust.Play();
    }
}