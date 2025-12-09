using UnityEngine;

public class FlowerSpawner : MonoBehaviour
{
    public GameObject flowerPrefab;

    public float spawnDelay = 20f;   

    // position limits
    public float minX = 5f;
    public float maxX = 9f;
    public float minY = -2f;
    public float maxY = 2f;

    public float firstSpawnDelay = 10f;
private bool firstFlowerSpawned = false;

    private float timer = 0f;
    private bool hasFlowerOnScreen = false;

    void Start()
    {
       hasFlowerOnScreen = false;
    timer = -10f;  // wait 10 seconds before first spawn
    }

    void Update()
{
    // FIRST FLOWER LOGIC
    if (!firstFlowerSpawned)
    {
        timer += Time.deltaTime;

        if (timer >= firstSpawnDelay)
        {
            SpawnFlower();
            firstFlowerSpawned = true;
            timer = 0f;
        }

        return; // stop here so normal logic doesn't run yet
    }

    // NORMAL FLOWER LOGIC (after first one)
    if (hasFlowerOnScreen)
        return;

    timer += Time.deltaTime;

    if (timer >= spawnDelay)
    {
        SpawnFlower();
    }
}


    void SpawnFlower()
    {
        float x = Random.Range(minX, maxX);
        float y = Random.Range(minY, maxY);

        Vector3 spawnPos = new Vector3(x, y, 0f);
        Instantiate(flowerPrefab, spawnPos, Quaternion.identity);

        hasFlowerOnScreen = true; // we now have a flower
        timer = 0f;               // reset timer

    }

    //  when the bee collects the flower
    public void OnFlowerCollected()
    {
        hasFlowerOnScreen = false;
       
    }
}
