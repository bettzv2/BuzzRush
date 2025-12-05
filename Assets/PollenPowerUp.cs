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
            Destroy(gameObject);                  // remove flower
        }
    }
}
