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
    private bool isCrouching;
    public float jumpForce;

    public float stopThreshold;

    private Rigidbody2D rb;
    private Animator animator;

    const String ANIM_STAND = "Stand";
    const String ANIM_WALK = "Walk";
    const String ANIM_RUN = "Run";
    const String ANIM_CROUCH = "Crouch";
    const String ANIM_AIR = "Air";

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

        if(rb.velocity.x < -0.001) {
            GetComponent<SpriteRenderer>().flipX = true;
        } else if(rb.velocity.x > 0.001) {
            GetComponent<SpriteRenderer>().flipX = false;
        }

        isCrouching = (Input.GetKey("down") || Input.GetKey("s")) ? true : false;

        setAnimations();
    }

    void FixedUpdate() {
        HInput = Input.GetAxis("Horizontal");
        //HInput = HInput == 0 ? 0 : Mathf.Sign(HInput);

        float directionMultiplier = Mathf.Abs(HInput) < stopThreshold ? 0 : Mathf.Sign(rb.velocity.x);

        //float friction = Mathf.Abs(rb.velocity.x) < 0.1 ? groundFriction * 1.5f : groundFriction;

        float newVelocity = Math.Clamp(rb.velocity.x + (HInput * acceleration * (isCrouching ? 0 : 1)) 
                                        - ((onGround ? 1 : 0) * directionMultiplier * groundFriction)
                                        , -1 * maxSpeed, maxSpeed);
        
        newVelocity = Mathf.Abs(newVelocity) < 0.0001 ? 0 : newVelocity;

        rb.velocity = new Vector2(newVelocity, rb.velocity.y);

    }

    private void setAnimations() {
        if (isCrouching) {
            ChangeAnimationState(ANIM_CROUCH);
        }else if (onGround) {
            float absVelocityX = Mathf.Abs(rb.velocity.x);

            if (absVelocityX < 0.001) {
                ChangeAnimationState(ANIM_STAND);
            } else if (absVelocityX < 2) {
                ChangeAnimationState(ANIM_WALK);
            } else {
                ChangeAnimationState(ANIM_RUN);
            }
        } else {
            ChangeAnimationState(ANIM_AIR);
        }
    }

    void ChangeAnimationState(string newState) {
        animator.Play(newState);
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
