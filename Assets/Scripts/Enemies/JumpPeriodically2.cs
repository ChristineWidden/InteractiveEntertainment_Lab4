using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System;

public class JumpPeriodically2 : MonoBehaviour
{
    //https://youtu.be/nPigL-dIqgE
    //https://www.youtube.com/watch?v=SPe1xh4D7Wg

    public float acceleration;
    public float maxSpeed;
    private bool onGround;
    // [SerializeField] private float groundedBuffer = 0;
    // private float groundedBufferMax;
    // private bool isCrouching;

    public float jumpHeight;
    // public float jumpTime;
    // public float jumpForce => (2f * jumpHeight) / (jumpTime / 2f);
    // public float gravity => (-2f * jumpHeight) / Mathf.Pow(jumpTime / 2f, 2f);

    private Vector2 velocity = new Vector2(0, 0);

    private Rigidbody2D rb;

    [SerializeField] private float gravityValue = -9.81f;

    // private int groundCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        
        // HInput = playerInput.actions["Movement"].ReadValue<float>();

        // velocity.x = Mathf.MoveTowards(velocity.x, HInput * maxSpeed, acceleration * maxSpeed * Time.deltaTime);


        if (onGround && velocity.y < 0)
        {
            velocity.y = 0f; // if on the ground, y velocity is 0
        }

        bool jumping = true;
        // Changes the height position of the player..
        if (jumping && onGround && velocity.y <= 0)
        {
            // Debug.Log("JUMPING!");
            velocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }
        velocity.y += gravityValue * Time.deltaTime; // apply gravity


        if(velocity.x < 0) {
            GetComponent<SpriteRenderer>().flipX = true;
        } else if(velocity.x > 0) {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        

        Vector2 position = rb.position;
        position += velocity * Time.deltaTime;

        rb.MovePosition(position);

    }


    void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Ground")) {
            onGround = true;
            //groundCount++;
            //groundedBuffer = groundedBufferMax;
            // Debug.Log("On ground");
        }
    }

    void OnCollisionExit2D(Collision2D other) {
        //if(other.gameObject.CompareTag("Ground") && groundedBuffer <= 0) {
        if(other.gameObject.CompareTag("Ground")) {
            // groundCount--;
            // if (groundCount == 0) {
            onGround = false;
            // Debug.Log("In air");
            // }
        }
    }


}
