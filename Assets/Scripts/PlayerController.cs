using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class PlayerController : MonoBehaviour
{
    //https://youtu.be/nPigL-dIqgE
    //https://www.youtube.com/watch?v=SPe1xh4D7Wg

    public float acceleration;
    public float maxSpeed;
    private float HInput;
    private bool onGround;
    private bool isCrouching;

    public float jumpHeight;
    public float jumpTime;
    public float jumpForce => (2f * jumpHeight) / (jumpTime / 2f);
    public float gravity => (-2f * jumpHeight) / Mathf.Pow(jumpTime / 2f, 2f);
    
    public int animatingHurt;

    public int maxHealth;
    public int currentHealth;
    public HealthBar healthBar;

    private Vector2 velocity = new Vector2(0, 0);

    private Rigidbody2D rb;
    private Animator animator;

    const String ANIM_STAND = "Stand";
    const String ANIM_WALK = "Walk";
    const String ANIM_RUN = "Run";
    const String ANIM_CROUCH = "Crouch";
    const String ANIM_AIR = "Air";
    const String ANIM_HURT = "Hurt";

    const int DAMAGE = 5;


    private int rockCountdown;

    public int ROCK_THROW_WAIT = 30;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        animatingHurt = 0;
        rockCountdown = 0;
    }

    // Update is called once per frame
    void Update()
    {

        if (rockCountdown > 0) {
            rockCountdown -= 1;
        }

        HInput = isCrouching ? 0 : Input.GetAxis("Horizontal");
        
        velocity.x = Mathf.MoveTowards(velocity.x, HInput * maxSpeed, acceleration * maxSpeed * Time.deltaTime);

        if(Input.GetKey("w")) {
            ThrowRock();
        }

        if (onGround) {
            velocity.y = Mathf.Max(velocity.y, 0f);
            if (Input.GetButtonDown("Jump")) {
                velocity.y = jumpForce;
            }
        }

        bool falling = velocity.y < 0f || !Input.GetButton("Jump");
        velocity.y += gravity * (falling ? 2f : 1f) * Time.deltaTime;
        velocity.y = Mathf.Max(velocity.y, gravity / 2f);



        if(velocity.x < 0) {
            GetComponent<SpriteRenderer>().flipX = true;
        } else if(velocity.x > 0) {
            GetComponent<SpriteRenderer>().flipX = false;
        }

        isCrouching = (onGround && (Input.GetKey("down") || Input.GetKey("s"))) ? true : false;

        setAnimations();
    }


    void FixedUpdate() {
        Vector2 position = rb.position;
        position += velocity * Time.fixedDeltaTime;

        if (animatingHurt > 0) {
            animatingHurt = animatingHurt - 1;
        }

        rb.MovePosition(position);
    }

    private void setAnimations() {
        if (animatingHurt != 0) {
            return;
        }

        if (isCrouching) {
            ChangeAnimationState(ANIM_CROUCH);
        }else if (onGround) {
            float absVelocityX = Mathf.Abs(velocity.x);

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

    //Upon collision with another GameObject, this GameObject will reverse direction
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (animatingHurt == 0 && other.gameObject.CompareTag("Damage")) {
            animatingHurt = 15;

            currentHealth -= DAMAGE;
            Debug.Log("Health is now " + currentHealth.ToString());

            ChangeAnimationState(ANIM_HURT);
            if (currentHealth < 1){
                SceneManager.LoadScene("GameOverScene");
            }

            healthBar.SetHealth(currentHealth);
            

        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if(other.gameObject.CompareTag("Ground")) {
            onGround = false;
            Debug.Log("In air");
        }
    }

    public void ThrowRock() {
        if (rockCountdown != 0) return;

        rockCountdown = ROCK_THROW_WAIT;

        float arrowDirectionX = transform.position.x + 0;
        float arrowDirectionY = transform.position.y + 1;

        Vector3 arrowMoveVector = new Vector3(arrowDirectionX, arrowDirectionY, 0f);
        Vector2 rockDirection = (arrowMoveVector - transform.position).normalized;

        GameObject rock = RockPool.rockPoolInstance.GetRock();
        rock.transform.position = transform.position;
        rock.transform.rotation = transform.rotation;
        rock.SetActive(true);
        rock.GetComponent<RockScript>().SetMoveDirection(rockDirection);
        
    }

}
