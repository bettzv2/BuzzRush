using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private MeshRenderer meshRenderer;

    public float animationSpeed = 1f;
    private Vector2 offset = Vector2.zero;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        // get global weather speed multiplier
        float mult = 1f;
        if (WeatherManager.Instance != null)
            mult = WeatherManager.Instance.worldSpeedMultiplier;

        // move texture using the multiplier
        offset.x += animationSpeed * mult * Time.deltaTime;

        meshRenderer.material.mainTextureOffset = offset;
    }
}
