using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System;

public class Physics : IOptionObserver
{
    //https://youtu.be/nPigL-dIqgE
    //https://www.youtube.com/watch?v=SPe1xh4D7Wg

    private Rigidbody2D rb;

    [SerializeField] private float jumpHeight;
    [SerializeField] private float acceleration;
    [SerializeField] private float decelerationMultiplier = 1;

    [SerializeField] private float maxSpeedBase;
    private float maxSpeed;

    [SerializeField] private float gravityValue = -9.81f;

    [SerializeField] private bool usesSpecialGroundCollider;
    public bool onGround { get; private set; }
    private int numGrounds = 0;

    public bool facingRight;
    public Vector2 velocity = new(0, 0);
    private Vector2 prevPosition = new(0, 0);
    public Vector2 trueVelocity = new(0, 0);

    [HideInInspector] public float HInput;
    [HideInInspector] public float CrouchInput;
    [HideInInspector] public float JumpInput;

    private float speedMultiplier = 1;

    public UnityEvent onJump;
    public UnityEvent onLeaveGround;
    public UnityEvent onHitGround;
    public UnityEvent onMoveLeft;
    public UnityEvent onMoveRight;
    public UnityEvent onFalling;


    private new void OnEnable()
    {
        speedMultiplier = 1;
        base.OnEnable();
        UpdateDifficulty();

        prevPosition = transform.position.xy();
    }
    public override void OnOptionChanged()
    {
        UpdateDifficulty();
    }
    public void UpdateDifficulty()
    {
        maxSpeed = maxSpeedBase * speedMultiplier;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {

        if (OptionsManager.Instance.IsPaused) return;


        if (onGround && (CrouchInput > 0.5f))
        {
            // if crouching, slow to a halt
            velocity.x = Mathf.MoveTowards(velocity.x, 0, 
                        decelerationMultiplier * acceleration * maxSpeed * Time.deltaTime);
        }
        else if ((HInput / Math.Abs(HInput)) * (velocity.x / Math.Abs(velocity.x)) < 0) {
            velocity.x = Mathf.MoveTowards(velocity.x, HInput * maxSpeed, 
                        decelerationMultiplier * acceleration * maxSpeed * Time.deltaTime);
        }
        else
        {
            velocity.x = Mathf.MoveTowards(velocity.x, HInput * maxSpeed, 
                        acceleration * maxSpeed * Time.deltaTime);
        }

        if (velocity.y < 0)
        {
            if (onGround)
            {
                velocity.y = 0f; // if on the ground, y velocity is 0
            }
            else
            {
                onFalling.Invoke();
            }
        }

        bool jumping = JumpInput > 0.5f;
        // Changes the height position of the player..
        if (jumping && onGround && velocity.y <= 0)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            onJump.Invoke();
        }
        velocity.y += gravityValue * Time.deltaTime; // apply gravity

        if (HInput < 0) {
            facingRight = false;
            GetComponent<SpriteRenderer>().flipX = true;
        } else if (HInput > 0) {
            facingRight = true;
            GetComponent<SpriteRenderer>().flipX = false;
        }

        if (velocity.x < 0)
        {
            onMoveLeft.Invoke();
        }
        else if (velocity.x > 0)
        {
            onMoveRight.Invoke();
        }

        Vector2 position = rb.position;
        position += velocity * Time.deltaTime;

        rb.MovePosition(position);
        
        trueVelocity = (transform.position.xy() - prevPosition) / Time.deltaTime;
        prevPosition = transform.position.xy();
    }

    public void setOnGround(bool state)
    {
        // Debug.Log("Set on ground invoked with " + state);
        onGround = state;
        if (onGround)
        {
            onHitGround.Invoke();
        }
        else
        {
            onLeaveGround.Invoke();
        }
    }

    public void setSpeedMultiplier(float speed) {
        speedMultiplier = speed;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground") && !usesSpecialGroundCollider)
        {
            onGround = true;
            if (numGrounds == 0) {
                onHitGround.Invoke();
            }
            numGrounds++;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground") && !usesSpecialGroundCollider)
        {
            numGrounds--;
            if (numGrounds == 0) {
                onGround = false;
                onLeaveGround.Invoke();
            }
        }
    }

    public float GetMaxSpeed() {
        return maxSpeed;
    }
}
