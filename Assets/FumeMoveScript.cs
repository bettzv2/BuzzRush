using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FumeMoveScript : MonoBehaviour
{
    public float moveSpeed = 10;

    void Update()
    {
        moveFume();
    }

    void moveFume()
    {
        
        float mult = 1f;
        if (WeatherManager.Instance != null)
            mult = WeatherManager.Instance.worldSpeedMultiplier;

        // move faster/slower depending on weather
        transform.position += Vector3.left * moveSpeed * mult * Time.deltaTime;

        if (transform.position.x < -32)
        {
            Destroy(gameObject);
        }
    }
}
