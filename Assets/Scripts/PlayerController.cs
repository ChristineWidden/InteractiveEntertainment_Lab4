using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    //https://youtu.be/nPigL-dIqgE

    public float acceleration;
    public float maxSpeed;
    public float groundFriction;
    public float HInput;

    private bool onGround;
    public float jumpForce;

    public float stopThreshold;

    private Rigidbody2D rb;
    private Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Jump") && onGround) {
            rb.AddForce(new Vector2(rb.velocity.x, jumpForce));
        }

        setAnimations();
    }

    void FixedUpdate() {
        HInput = Input.GetAxis("Horizontal");
        //HInput = HInput == 0 ? 0 : Mathf.Sign(HInput);

        float directionMultiplier = Mathf.Abs(HInput) < stopThreshold ? 0 : Mathf.Sign(rb.velocity.x);

        //float friction = Mathf.Abs(rb.velocity.x) < 0.1 ? groundFriction * 1.5f : groundFriction;

        float newVelocity = Math.Clamp(rb.velocity.x + (HInput * acceleration) 
                                        - ((onGround ? 1 : 0) * directionMultiplier * Mathf.Sqrt(groundFriction))
                                        , -1 * maxSpeed, maxSpeed);
        
        newVelocity = Mathf.Abs(newVelocity) < 0.0001 ? 0 : newVelocity;

        rb.velocity = new Vector2(newVelocity, rb.velocity.y);
        


    }

    private void setAnimations() {
        
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Ground")) {
            onGround = true;
            Debug.Log("On ground");
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if(other.gameObject.CompareTag("Ground")) {
            onGround = false;
            Debug.Log("In air");
        }
    }

}
