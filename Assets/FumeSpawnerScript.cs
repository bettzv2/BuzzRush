using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FumeSpawnerScript : MonoBehaviour
{
    public GameObject fume;
    public float spawnRate = 2f;   // base spawn interval in seconds
    private float timer = 0f;
    public float heightOffset = 0f;

    void Start()
    {
        spawnFume();
    }

    void Update()
    {
        //  get weather speed multiplier 
        float mult = 1f;
        if (WeatherManager.Instance != null)
            mult = WeatherManager.Instance.worldSpeedMultiplier;

        // advance timer with normal time
        timer += Time.deltaTime;

        // spawn rate
        float effectiveSpawnRate = spawnRate / mult;

        if (timer >= effectiveSpawnRate)
        {
            spawnFume();
            timer = 0f;
        }
    }

    void spawnFume()
    {
        float lowestPoint = transform.position.y - heightOffset;
        float highestPoint = transform.position.y + heightOffset;

        Instantiate(
            fume,
            new Vector3(
                transform.position.x,
                Random.Range(lowestPoint, highestPoint)
            ),
            transform.rotation
        );
    }
}
