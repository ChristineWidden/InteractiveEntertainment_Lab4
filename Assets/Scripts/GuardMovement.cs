using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardMovement : MonoBehaviour
{

    public float moveSpeed;
    private bool moveRight;
    public float moveLimit;

    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = 2f;
        moveRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > moveLimit) {
            moveRight = false;
        } else if (transform.position.x < moveLimit * -1) {
            moveRight = true;
        }

        transform.position = new Vector2(transform.position.x + (moveRight ? 1 : -1) * moveSpeed * Time.deltaTime, transform.position.y);
    }
}
