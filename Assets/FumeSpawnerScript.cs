using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FumeSpawnerScript : MonoBehaviour
{
    public GameObject fume;
    public float spawnRate = 2;
    private float timer = 0;
    public float heightOffset = 0;

    // Start is called before the first frame update
    void Start()
    {
        spawnFume();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < spawnRate)
        {
            timer += Time.deltaTime;
        }
        else {
            spawnFume();
            timer = 0;
        }
        
        
    }

    void spawnFume()
    {
        float lowestPoint = transform.position.y - heightOffset;
        float highestPoint = transform.position.y + heightOffset;
        Instantiate(fume,
                    new Vector3(transform.position.x,
                    Random.Range(lowestPoint, highestPoint)),
                    transform.rotation);
    }

}
