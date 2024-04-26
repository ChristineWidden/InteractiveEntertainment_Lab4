using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System;

public class Physics : MonoBehaviour, IOptionObserver
{
    //https://youtu.be/nPigL-dIqgE
    //https://www.youtube.com/watch?v=SPe1xh4D7Wg

    private Rigidbody2D rb;

    [SerializeField] private float jumpHeight;
    [SerializeField] private float acceleration;

    [SerializeField] private float maxSpeedBase;
    private float maxSpeed;
    
    [SerializeField] private float gravityValue = -9.81f;

    private bool onGround;
    [HideInInspector] public bool facingRight;
    [HideInInspector] public Vector2 velocity = new(0, 0);

    [HideInInspector] public float HInput;
    [HideInInspector] public float CrouchInput;
    [HideInInspector] public float JumpInput;

    public UnityEvent onJump;
    public UnityEvent onLeaveGround;
    public UnityEvent onHitGround;
    public UnityEvent onMoveLeft;
    public UnityEvent onMoveRight;
    public UnityEvent onFalling;


    private void OnEnable()
    {
        OptionsManager.Instance.RegisterObserver(this);
        UpdateDifficulty();
    }
    private void OnDisable()
    {
        OptionsManager.Instance.UnregisterObserver(this);
    }
    public void OnOptionChanged() {
        UpdateDifficulty();
    }
    private void UpdateDifficulty() {
        maxSpeed = maxSpeedBase * OptionsManager.Instance.currentDifficulty.enemySpeedMultiplier;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        
        if (onGround && (CrouchInput > 0.5f)) {
            // if crouching, slow to a halt
            Debug.Log("hi");
            velocity.x = Mathf.MoveTowards(velocity.x, 0, acceleration * maxSpeedBase * Time.deltaTime);
        } else {
            velocity.x = Mathf.MoveTowards(velocity.x, HInput * maxSpeedBase, acceleration * maxSpeedBase * Time.deltaTime);
        }

        if (velocity.y < 0) {
            if (onGround) {
                velocity.y = 0f; // if on the ground, y velocity is 0
            } else {
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


        if(velocity.x < 0 && facingRight) {
            facingRight = false;
            GetComponent<SpriteRenderer>().flipX = true;
            onMoveLeft.Invoke();
        } else if(velocity.x > 0 && !facingRight) {
            facingRight = true;
            GetComponent<SpriteRenderer>().flipX = false;
            onMoveRight.Invoke();
        }
        
        Vector2 position = rb.position;
        position += velocity * Time.deltaTime;

        rb.MovePosition(position);
    }


    void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Ground")) {
            onGround = true;
            onHitGround.Invoke();
        }
    }

    void OnCollisionExit2D(Collision2D other) {
        if(other.gameObject.CompareTag("Ground")) {

            onGround = false;
            onLeaveGround.Invoke();
        }
    }
}
