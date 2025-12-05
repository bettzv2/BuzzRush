using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FumeSpawnerScript : MonoBehaviour
{
    public GameObject fume;
    public float spawnRate = 2f;   // base spawn interval in seconds
    private float timer = 0f;

    public float heightOffset = 0f;

    // top and bottom fume spawn 
    public float minSpawnY = -3f;   // lowest it can spawn
    public float maxSpawnY =  3f;   // highest it can spawn

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
        // base spawn range
        float lowestPoint = transform.position.y - heightOffset;
        float highestPoint = transform.position.y + heightOffset;

        // pick a random Y
        float randomY = Random.Range(lowestPoint, highestPoint);

        // CLAMP it so it never goes out of bounds
        randomY = Mathf.Clamp(randomY, minSpawnY, maxSpawnY);

        Instantiate(
            fume,
            new Vector3(transform.position.x, randomY),
            transform.rotation
        );
    }
}
