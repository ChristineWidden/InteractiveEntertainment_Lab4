using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System;

public class PlayerController : MonoBehaviour
{

    public int points = 0;    

    public int maxHealth;
    public int currentHealth;
    public HealthMeterScript healthBar;

    //private Animator animator;

    const String ANIM_STAND = "Stand";
    const String ANIM_WALK = "Walk";
    const String ANIM_RUN = "Run";
    const String ANIM_CROUCH = "Crouch";
    const String ANIM_AIR = "Air";
    const String ANIM_HURT = "Hurt";

    [SerializeField] private const int DAMAGE = 1;

    private int rockCountdown;

    public int ROCK_THROW_WAIT;

    [SerializeField] private AudioSource jumpSoundEffect;
    [SerializeField] private AudioSource hurtSoundEffect;
    [SerializeField] private AudioSource throwSoundEffect;

    private PlayerAnimator animator;

    private PlayerInput playerInput;

    [SerializeField] private float immunityDuration;
    private float immunityCountdown;

    [SerializeField] private GameObject projectile;

    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        animator = GetComponent<PlayerAnimator>();

        //animator = GetComponent<Animator>();
        currentHealth = maxHealth;

        healthBar.SetMaxHealth(maxHealth);
        healthBar.SetHealth(maxHealth);
        immunityCountdown = 0f;
        rockCountdown = 0;
    }

    void Update()
    {

        if (rockCountdown > 0) {
            rockCountdown -= 1;
        }

        if(playerInput.actions["ThrowRock"].ReadValue<float>() > 0.5f) {
            Debug.Log("Throwing a rock!");
            ThrowRock();
        }

        animator.animatingHurt = immunityCountdown;

        if (immunityCountdown > 0) {
            immunityCountdown -= Time.deltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (immunityCountdown <= 0 && other.gameObject.CompareTag("Damage")) {
            immunityCountdown = immunityDuration;

            currentHealth -= DAMAGE;
            //Debug.Log("Health is now " + currentHealth.ToString());
            hurtSoundEffect.Play();

            if (currentHealth < 1){
                SceneManager.LoadScene("GameOverScene");
            }

            healthBar.SetHealth(currentHealth);
            
        }
    }

    public void ThrowRock() {
        if (rockCountdown != 0) return;

        throwSoundEffect.Play();

        rockCountdown = ROCK_THROW_WAIT;

        float arrowDirectionX = transform.position.x + 0;
        float arrowDirectionY = transform.position.y + 1;

        Vector3 arrowMoveVector = new(arrowDirectionX, arrowDirectionY, 0f);
        Vector2 rockDirection = (arrowMoveVector - transform.position).normalized;

        //GameObject rock = RockPool.rockPoolInstance.GetRock();
        GameObject rock = Instantiate(projectile);
        rock.transform.position = transform.position;
        rock.transform.rotation = transform.rotation;
        rock.SetActive(true);
        rock.GetComponent<PlayerProjectile>().moveDirection = rockDirection;
    }

}
