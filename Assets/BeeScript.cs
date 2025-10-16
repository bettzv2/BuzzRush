using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeScript : MonoBehaviour
{
    public Rigidbody2D rbody;
    public float flapStrength = 18f;     // tap W burst 
    public float holdThrust = 2.5f;     // hold W to float a little
    public float diveForce  = 4f;       // hold S to dive
    public float horizSpeed = 2.5f;     // A/D horizontal drift
    public float maxYSpeed  = 13f;       // cap vertical speed 

    public LogicScript logic;
    public bool beeIsAlive = true;
    public AudioSource flyAudio;

    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        // keep gravity on the Rigidbody2D
    }

    void Update()
    {
        if (!beeIsAlive) return;

        // Vertical control 
        if (Input.GetKeyDown(KeyCode.W)) {
            // flap-like burst upward (keep current x velocity)
            rbody.velocity = new Vector2(rbody.velocity.x, flapStrength);
        }

        // gentle hover while holding W
        if (Input.GetKey(KeyCode.W)) {
            rbody.AddForce(Vector2.up * holdThrust, ForceMode2D.Force);
        }

        // dive while holding S
        if (Input.GetKey(KeyCode.S)) {
            rbody.AddForce(Vector2.down * diveForce, ForceMode2D.Force);
        }

        // Cap vertical speed
        rbody.velocity = new Vector2(
            rbody.velocity.x,
            Mathf.Clamp(rbody.velocity.y, -maxYSpeed, maxYSpeed)
        );

        // Horizontal drift with A/D 
        int h = 0;
        if (Input.GetKey(KeyCode.A)) h -= 2;
        if (Input.GetKey(KeyCode.D)) h += 2;

        // set horizontal component while preserving the (possibly updated) vertical velocity
        rbody.velocity = new Vector2(h * horizSpeed, rbody.velocity.y);
    }

    void OnBecameInvisible()
    {
        beeIsAlive = false;
        logic.gameOver();
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer != 6) {
            beeIsAlive = false;
            logic.gameOver();
        }
    }


}
