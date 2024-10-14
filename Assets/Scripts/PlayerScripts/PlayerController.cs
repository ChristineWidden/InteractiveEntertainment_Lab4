using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PlayerController : IOptionObserver
{

    // Directly attached Scripts
    private Physics physics;
    private PlayerAnimator animator;
    private PlayerInput playerInput;

    private GameObject projectile;
    [SerializeField] private GameObject defaultProjectile;


    // Indirectly attached stuff
    [SerializeField] private SpecialCollider rayTrace;
    [SerializeField] private SpecialCollider hurtbox;


    // Distantly attached stuff
    public HealthRing healthBar;
    public PowerUpUI powerUpUI;

    [SerializeField] private AudioSource jumpSoundEffect;
    [SerializeField] private AudioSource hurtSoundEffect;
    [SerializeField] private AudioSource throwSoundEffect;


    // Current Status
    public int points = 0;

    public int maxHealth;
    public int currentHealth;

    public PowerUpEnum powerUpState = PowerUpEnum.Rock;

    [SerializeField] private const int DAMAGE = 1;


    // Options & Controls
    private bool throwRockToggle = false;
    private float throwRockToggleTimer;


    // Timers & Such
    private float immunitySeconds;
    private float immunityTimer;
    private float ROCK_THROW_WAIT;
    public float ROCK_THROW_WAIT_BASE;
    private float rockCountdown;
    [SerializeField] private float immunitySecondsBase;


    // Other
    private Coroutine resetPowerUpCoroutine;



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
        physics = GetComponent<Physics>();
        projectile = defaultProjectile;

        currentHealth = maxHealth;

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
            animator.animatingHurt = true;
            immunityTimer -= Time.deltaTime;
        } else {
            animator.animatingHurt = false;
            if (hurtbox.colliding) {
                GetHurt();
            }
        }

        if (throwRockToggleTimer > 0)
        {
            throwRockToggleTimer -= Time.deltaTime;
        }

        if (physics.facingRight)
        {
            rayTrace.gameObject.transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            rayTrace.gameObject.transform.localScale = new Vector3(-1, 1, 1);
        }
        bool autoFireRock = OptionsManager.Instance.GetBooleanOption(BooleanOptionEnum.AUTO_FIRE_ON)
                            && rayTrace.numCollisions > 0;
        bool doThrowRock = autoFireRock || (playerInput.actions["ThrowRock"].ReadValue<float>() > 0.5f);

        if (playerInput.actions["ToggleThrowRock"].ReadValue<float>() > 0.5f && throwRockToggleTimer <= 0)
        {
            throwRockToggle = !throwRockToggle;
            throwRockToggleTimer = 0.2f;
        }
        if ((throwRockToggle || doThrowRock) && rockCountdown <= 0)
        {
            ThrowRock();
        }

    }

    public void GetHurt()
    {
        if (immunityTimer > 0) return;

        immunityTimer = immunitySeconds;
        currentHealth -= DAMAGE;
        hurtSoundEffect.Play();
        if (currentHealth < 1)
        {
            SceneManager.LoadScene("GameOverScene");
        }
        healthBar.SetHealth(currentHealth);
    }

    public void HandleFeetCollisions(Collider2D collider)
    {
        string otherTag = collider.gameObject.tag;

        // Debug.Log("handling feet collisions with " + otherTag);

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

    public void HandlePowerUpCollisions(Collider2D collider)
    {
        // Debug.Log("handling powerup collisions with ", collider);

        PowerUp powerUp = collider.GetComponent<PowerUp>();
        powerUpState = powerUp.powerUpType;
        powerUpUI.SetPowerUp(powerUpState);
        projectile = powerUp.associatedProjectile;

        if (resetPowerUpCoroutine != null)
        {
            StopCoroutine(resetPowerUpCoroutine);
        }
        resetPowerUpCoroutine = StartCoroutine(ResetPowerUp(powerUp.powerUpDuration));
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
        // Debug.Log("Throwing a rock!");

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
