using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System;

public class PlayerController : IOptionObserver
{

    public int points = 0;

    public int maxHealth;
    public int currentHealth;
    public HealthRing healthBar;
    public PowerUpUI powerUpUI;

    public PowerUpEnum powerUpState = PowerUpEnum.Rock;

    private Coroutine resetPowerUpCoroutine;

    private Physics physics;

    //private Animator animator;

    const String ANIM_STAND = "Stand";
    const String ANIM_WALK = "Walk";
    const String ANIM_RUN = "Run";
    const String ANIM_CROUCH = "Crouch";
    const String ANIM_AIR = "Air";
    const String ANIM_HURT = "Hurt";

    [SerializeField] private const int DAMAGE = 1;


    [SerializeField] private AudioSource jumpSoundEffect;
    [SerializeField] private AudioSource hurtSoundEffect;
    [SerializeField] private AudioSource throwSoundEffect;

    private PlayerAnimator animator;
    private PlayerInput playerInput;

    private float ROCK_THROW_WAIT;
    public float ROCK_THROW_WAIT_BASE;
    private float rockCountdown;
    [SerializeField] private float immunitySecondsBase;
    private float immunitySeconds;
    private float immunityTimer;


    private GameObject projectile;
    [SerializeField] private GameObject defaultProjectile;

    public bool facingRight = true;


    private new void OnEnable()
    {
        base.OnEnable();
        UpdateDifficulty();
    }
    public override void OnOptionChanged()
    {
        UpdateDifficulty();
    }
    private void UpdateDifficulty()
    {
        immunitySeconds = immunitySecondsBase * OptionsManager.Instance.currentDifficulty.playerImmunityFrameMultiplier;
        ROCK_THROW_WAIT = ROCK_THROW_WAIT_BASE * OptionsManager.Instance.currentDifficulty.playerProjectileFrequencyMultiplier;
    }


    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        animator = GetComponent<PlayerAnimator>();
        physics = GetComponent<Physics>(); // TODO switch over to universal physics script
        projectile = defaultProjectile;

        //animator = GetComponent<Animator>();
        currentHealth = maxHealth;

        // healthBar.SetMaxHealth(maxHealth);
        healthBar.SetHealth(maxHealth);
        immunityTimer = 0f;
        rockCountdown = 0;
    }

    void Update()
    {

        if (rockCountdown > 0)
        {
            rockCountdown -= Time.deltaTime;
        }
        if (immunityTimer > 0)
        {
            immunityTimer -= Time.deltaTime;
        }


        if (playerInput.actions["ThrowRock"].ReadValue<float>() > 0.5f && rockCountdown <= 0)
        {
            ThrowRock();
        }

    }

    private void GetHurt()
    {
        immunityTimer = immunitySeconds;
        currentHealth -= DAMAGE;
        hurtSoundEffect.Play();
        if (currentHealth < 1)
        {
            SceneManager.LoadScene("GameOverScene");
        }
        healthBar.SetHealth(currentHealth);
    }

    public void HandleFeetCollisions(string otherTag)
    {
        Debug.Log("handling feet collisions with " + otherTag);

        if (otherTag == "Enemy")
        {
            Debug.Log("Adding velocity!");
            physics.velocity = new Vector2(physics.velocity.x, 10);
        }

        if (otherTag == "Damage" || otherTag == "Enemy"
        && immunityTimer <= 0)
        {
            GetHurt();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.gameObject.CompareTag("Damage") || other.gameObject.CompareTag("Enemy"))
            && immunityTimer <= 0)
        {
            GetHurt();
        }

        if (other.gameObject.CompareTag("PowerUp"))
        {
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

    private IEnumerator ResetPowerUp(float powerUpDuration)
    {
        yield return new WaitForSeconds(powerUpDuration);
        powerUpState = PowerUpEnum.Rock;
        projectile = defaultProjectile;
        powerUpUI.SetPowerUp(powerUpState);
    }

    public void ThrowRock()
    {
        Debug.Log("Throwing a rock!");

        throwSoundEffect.Play();

        rockCountdown = ROCK_THROW_WAIT;

        float arrowDirectionX = transform.position.x + (physics.facingRight ? 1 : -1);
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

    public void Jump()
    {
        jumpSoundEffect.Play();
    }

}
