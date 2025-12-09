using UnityEngine;

public class PollenPowerUp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        BeeScript bee = other.GetComponent<BeeScript>();
        if (bee != null)
        {
            bee.hasPollen = true;                // activate powerup
            bee.pollenTimer = bee.pollenDuration; // start countdown

            // tell the spawner that this flower was collected
            FlowerSpawner spawner = FindObjectOfType<FlowerSpawner>();
            if (spawner != null)
            {
                spawner.OnFlowerCollected();
            }

            Destroy(gameObject);                  // remove flower
        }
    }
}
