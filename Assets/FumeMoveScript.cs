using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FumeMoveScript : MonoBehaviour
{

    public float moveSpeed = 10;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        moveFume();
    }

    void moveFume() {
        transform.position = transform.position + (Vector3.left * moveSpeed) * Time.deltaTime;

        if (transform.position.x < -32)
        {
            Destroy(gameObject);
        }
    }
}
