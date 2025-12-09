using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeScript : MonoBehaviour
{
    // pollen burst immunity
    public bool hasPollen = false; 
    public float pollenDuration = 4f;   // how long it lasts
    public float pollenTimer = 0f;      // countdown timer

    private SpriteRenderer sr;          // to change bee color
    private Collider2D beeCol; 

    public Rigidbody2D rbody;
    public float flapStrength = 18f;     // tap W burst 
    public float holdThrust = 2.5f;      // hold W to float a little
    public float diveForce  = 4f;        // hold S to dive
    public float horizSpeed = 2.5f;      // A/D horizontal drift
    public float maxYSpeed  = 13f;       // cap vertical speed 

    public float speedMultiplier = 1f; 
    public float windForce = 0f;      
    public float rainSlow = 1f;

    public LogicScript logic;
    public bool beeIsAlive = true;
    public AudioSource flyAudio;

    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        sr = GetComponent<SpriteRenderer>();
        beeCol = GetComponent<Collider2D>();   
    }

    void Update()
    {
        if (!beeIsAlive) return;

        // Vertical control 
        // gentle hover while holding W
        if (Input.GetKey(KeyCode.W)) {
            rbody.velocity = new Vector2(rbody.velocity.x, flapStrength);
        }

        // dive while holding S
        if (Input.GetKey(KeyCode.S)) {
            rbody.velocity = new Vector2(rbody.velocity.x, -diveForce);
        }

        // Cap vertical speed
        rbody.velocity = new Vector2(
            rbody.velocity.x,
            Mathf.Clamp(rbody.velocity.y, -maxYSpeed, maxYSpeed)
        );
        // Horizontal drift with A/D, affected by weather + wind 
        int h = 0;
        if (Input.GetKey(KeyCode.A)) h -= 1;
        if (Input.GetKey(KeyCode.D)) h += 1;

        float speed = horizSpeed * speedMultiplier;
        Vector2 v = rbody.velocity;

        float inputX = h * speed;
        float windX = windForce;

        v.x = inputX + windX;
        rbody.velocity = v;

        //Pollen Burst timer and glow
        if (hasPollen)
        {
            pollenTimer -= Time.deltaTime;

            if (sr != null)
                sr.color = Color.yellow;   // glow while powered

            if (pollenTimer <= 0f)
                EndPollen();
        }
        else
        {
            if (sr != null)
                sr.color = Color.white;    // normal color
        }
    }

    void OnBecameInvisible()
    {
        beeIsAlive = false;
        logic.gameOver();
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If we are in pollen mode n we hit a fume allow it to pass
        if (hasPollen && collision.gameObject.CompareTag("Fume"))
        {
            StartCoroutine(PassThroughFumeOnce(collision.collider));
            return; // don't die
        }

        // Otherwise normal death rule
        if (collision.gameObject.layer != 6)
        {
            beeIsAlive = false;
            logic.gameOver();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Flower"))
        {
            StartPollen();
            Destroy(other.gameObject);
        }
    }

    void StartPollen()
    {
        hasPollen = true;
        pollenTimer = pollenDuration;
    }

    void EndPollen()
    {
        hasPollen = false;
        pollenTimer = 0f;
    }

    // ignore one fume for a short time then reenable collisions (maybe find a diff method idk)
    private IEnumerator PassThroughFumeOnce(Collider2D fumeCol)
    {
        EndPollen();  // use up the power

        Physics2D.IgnoreCollision(beeCol, fumeCol, true);
        yield return new WaitForSeconds(1.5f); // long enough to fly through
        Physics2D.IgnoreCollision(beeCol, fumeCol, false);
    }
}
