// File name   : DissapearingPlatform.cs
// Author(s)   : Minki Cho
// Copyright   : Copyright(C) 2025 DigiPen Institute of Technology
using UnityEngine;
using System.Collections;

public class DisappearingPlatform : MonoBehaviour
{
    [Header("Timings")]
    public float initialDelay = 3f;
    public float blinkDuration = 3f;
    public float blinkInterval = 0.2f;
    public float fallSpeed = 2f;
    public float respawnTime = 5f;

    private Renderer[] renderers;
    private Collider col;
    private Vector3 originalPosition;
    private bool triggered = false;

    void Start()
    {
        renderers = GetComponentsInChildren<Renderer>();
        col = GetComponent<Collider>();
        originalPosition = transform.position;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!triggered && collision.gameObject.CompareTag("Player"))
        {
            triggered = true;
            StartCoroutine(DisappearRoutine());
        }
    }

    IEnumerator DisappearRoutine()
    {

        yield return new WaitForSeconds(initialDelay);


        float timer = 0f;
        while (timer < blinkDuration)
        {
            ToggleRenderers(false);
            yield return new WaitForSeconds(blinkInterval);

            ToggleRenderers(true);
            yield return new WaitForSeconds(blinkInterval);

            timer += blinkInterval * 2;
        }


        col.enabled = false;

        float fallTimer = 0f;
        float fallTime = 1f;

        while (fallTimer < fallTime)
        {
            transform.position += Vector3.down * fallSpeed * Time.deltaTime;
            fallTimer += Time.deltaTime;
            yield return null;
        }


        ToggleRenderers(false);


        if (respawnTime > 0)
        {
            yield return new WaitForSeconds(respawnTime);


            transform.position = originalPosition;

            ToggleRenderers(true);
            col.enabled = true;
            triggered = false;
        }
    }

    void ToggleRenderers(bool state)
    {
        foreach (var r in renderers)
            r.enabled = state;
    }
}
