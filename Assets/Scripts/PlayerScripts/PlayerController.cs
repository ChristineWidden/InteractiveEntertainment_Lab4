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
    public HealthRing healthBar;
    public PowerUpUI powerUpUI;

    public PowerUpEnum powerUpState = PowerUpEnum.Rock;

    private Coroutine resetPowerUpCoroutine;


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

    private GameObject projectile;
    [SerializeField] private GameObject defaultProjectile;

    public bool facingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        animator = GetComponent<PlayerAnimator>();
        projectile = defaultProjectile;

        //animator = GetComponent<Animator>();
        currentHealth = maxHealth;

        // healthBar.SetMaxHealth(maxHealth);
        healthBar.SetHealth(maxHealth);
        immunityCountdown = 0f;
        rockCountdown = 0;
    }

    void Update()
    {

        if (rockCountdown > 0) {
            rockCountdown -= 1;
        }

        if(playerInput.actions["ThrowRock"].ReadValue<float>() > 0.5f && rockCountdown == 0) {
            ThrowRock();
        }

        if (immunityCountdown > 0) {
            immunityCountdown -= Time.deltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.gameObject.CompareTag("Damage") || other.gameObject.CompareTag("Enemy")) 
            && immunityCountdown <= 0) {
            immunityCountdown = immunityDuration;

            currentHealth -= DAMAGE;
            hurtSoundEffect.Play();

            if (currentHealth < 1){
                SceneManager.LoadScene("GameOverScene");
            }

            healthBar.SetHealth(currentHealth);
        }

        if (other.gameObject.CompareTag("Enemy")) {
            // TODO bounce back off of enemy
        }

        if (other.gameObject.CompareTag("PowerUp")) {
            PowerUp powerUp = other.GetComponent<PowerUp>();
            powerUpState = powerUp.powerUpType;
            powerUpUI.SetPowerUp(powerUpState);
            projectile = powerUp.associatedProjectile;

            if (resetPowerUpCoroutine != null)
            {
                StopCoroutine(resetPowerUpCoroutine);
            }
            resetPowerUpCoroutine = StartCoroutine(ResetPowerUp(powerUp.powerUpDuration));
            // Invoke(nameof(ResetPowerUp), powerUp.powerUpDuration);
        }
    }

    private IEnumerator ResetPowerUp(float powerUpDuration) {
        yield return new WaitForSeconds(powerUpDuration);
        powerUpState = PowerUpEnum.Rock;
        projectile = defaultProjectile;
        powerUpUI.SetPowerUp(powerUpState);
    }

    public void ThrowRock() {
        Debug.Log("Throwing a rock!");

        throwSoundEffect.Play();

        rockCountdown = ROCK_THROW_WAIT;

        float arrowDirectionX = transform.position.x + (facingRight ? 1 : -1);
        float arrowDirectionY = transform.position.y + 0;

        Vector3 arrowMoveVector = new(arrowDirectionX, arrowDirectionY, 0f);
        Vector2 rockDirection = (arrowMoveVector - transform.position).normalized;

        GameObject rock = Instantiate(projectile);
        rock.transform.position = transform.position;
        rock.transform.rotation = transform.rotation;
        rock.SetActive(true);
        rock.transform.parent = ProjectileHolder.instance.transform;

        rock.GetComponent<PlayerProjectile>().moveDirection = rockDirection;
    }

}
