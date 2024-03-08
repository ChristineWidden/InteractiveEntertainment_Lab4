using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System;

public class PlayerPhysics : MonoBehaviour
{
    //https://youtu.be/nPigL-dIqgE
    //https://www.youtube.com/watch?v=SPe1xh4D7Wg

    public float acceleration;
    public float maxSpeed;
    private float HInput;
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


    [SerializeField] private AudioSource jumpSoundEffect;
    [SerializeField] private AudioSource hurtSoundEffect;
    [SerializeField] private AudioSource throwSoundEffect;

    private PlayerInput playerInput;
    private PlayerAnimator animator;

    [SerializeField] private float gravityValue = -9.81f;

    private string nextAnimation;

    // private int groundCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<PlayerAnimator>();

    }

    void FixedUpdate()
    {
        
        HInput = playerInput.actions["Movement"].ReadValue<float>();
        if (onGround && (playerInput.actions["Crouch"].ReadValue<float>() > 0.5f)) {
            // if crouching, slow to a halt
            Debug.Log("hi");
            velocity.x = Mathf.MoveTowards(velocity.x, 0, acceleration * maxSpeed * Time.deltaTime);
        } else {
            velocity.x = Mathf.MoveTowards(velocity.x, HInput * maxSpeed, acceleration * maxSpeed * Time.deltaTime);
        }

        if (onGround && velocity.y < 0)
        {
            velocity.y = 0f; // if on the ground, y velocity is 0
        }

        bool jumping = playerInput.actions["Jump"].ReadValue<float>() > 0.5f;
        // Changes the height position of the player..
        if (jumping && onGround && velocity.y <= 0)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            jumpSoundEffect.Play();
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

        animator.onGround = onGround;
        animator.velocity = velocity;
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