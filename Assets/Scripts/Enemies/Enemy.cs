using UnityEngine;

public class Enemy : IOptionObserver
{    

    // TODO some way to make sure that stun timer resets with each hit

    [SerializeField] private float damageTakenOnHitBase;
    private float damageTakenOnHit;
    [SerializeField] private float immunitySecondsBase;

    private float immunitySeconds;
    private bool immune;
    private float immunityTimer;
    private bool stunned;
    private float stunTimer;

    private Collider2D thisCollider;
    private SpriteRenderer sprite;

    private Animator animator;
    private string currentAnimState;
    [SerializeField] private string defaultAnimState;
    [SerializeField] private string hurtAnimState;
    [SerializeField] private string idleAnimState;
    
    private float originalAlpha;
    private float stunAlpha;

    [SerializeField] private AudioSource hurtSoundEffect;


    private new void OnEnable()
    {
        base.OnEnable();
        UpdateDifficulty();
    }
    public override void OnOptionChanged() {
        UpdateDifficulty();
    }
    private void UpdateDifficulty() {
        immunitySeconds = immunitySecondsBase * OptionsManager.Instance.currentDifficulty.enemyImmunityFrameMultiplier;
        damageTakenOnHit = damageTakenOnHitBase * OptionsManager.Instance.currentDifficulty.enemyDamageMultiplier;
    }


    void Start()
    {
        thisCollider = GetComponent<Collider2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        ChangeAnimationState(defaultAnimState);
        originalAlpha = sprite.material.GetFloat("_Alpha");
        stunAlpha = originalAlpha * 0.5f;
        thisCollider.isTrigger = false; // Initially, collider acts as solid
        immunityTimer = 0;
        stunTimer = 0;
    }

    void Update()
    {
        if (immunityTimer > 0)
        {
            immunityTimer-= Time.deltaTime;
        }
        if (stunTimer > 0)
        {
            stunTimer -= Time.deltaTime;
        } else if (stunned) {
            StunOff();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (immunityTimer <= 0 && other.gameObject.CompareTag("PlayerProjectile"))
        {
            immunityTimer = 0;
            hurtSoundEffect.Play();

            immunityTimer = immunitySeconds;

            PlayerProjectile projectile = other.GetComponent<PlayerProjectile>();

            if (projectile.doesKill)
            {
                Debug.Log("DYING0!");
                Die();
            }
            else
            {
                if (projectile.stunTime > 0)
                {
                    GetStunned(projectile.stunTime);
                }
                
                if (projectile.freezeTime > 0) {
                    GetFrozen(projectile.freezeTime);
                }
            }
        }
    }

    void GetFrozen(float freezeTime) {
        GuardMovement2 guardMovement = gameObject.GetComponent<GuardMovement2>();
        if (guardMovement != null) {
            guardMovement.Freeze(freezeTime);
        }
        JumpPeriodically jumpPeriodically = gameObject.GetComponent<JumpPeriodically>();
        if (jumpPeriodically != null) {
            jumpPeriodically.Freeze(freezeTime);
        }
    }

    void GetStunned(float stunTime)
    {
        // Debug.Log("Transparent now?");
        // thisCollider.isTrigger = true;
        ChangeLayer("IgnorePlayer");
        sprite.material.SetFloat("_Alpha", stunAlpha);
        stunTimer = stunTime;
        stunned = true;
        // StartCoroutine(StunOff(stunTime));
    }

    void StunOff()
    {
        // yield return new WaitForSeconds(delayTime);
        sprite.material.SetFloat("_Alpha", originalAlpha);
        // thisCollider.isTrigger = false;
        ChangeLayer("Default");
        stunned = false;
    
    }

    void ChangeLayer(string layerName)
    {
        // Get the layer index by name
        int layerIndex = LayerMask.NameToLayer(layerName);
        
        // Check if the layer index is valid
        if (layerIndex != -1)
        {
            // Change the layer of the GameObject
            gameObject.layer = layerIndex;
            Debug.Log("Layer changed to: " + layerName);
        }
        else
        {
            Debug.LogError("Layer not found: " + layerName);
        }
    }

    void Die()
    {
        // TODO: Add death animation
        Debug.Log("DYING!");
        Destroy(gameObject);
    }

    public void ChangeAnimationState(string newState) {
        if (currentAnimState == newState) return;
        animator.Play(newState);
        currentAnimState = newState;
    }
}
