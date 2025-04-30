using UnityEngine;
using System.Collections;
public class Enemy : IOptionObserver
{

    [SerializeField] private float damageTakenOnHitBase;
    private float damageTakenOnHit;
    [SerializeField] private float immunitySecondsBase;

    [SerializeField] private SpriteRenderer freezeIconRenderer;
    [SerializeField] private Collider2D shield;


    private float immunitySeconds;
    private bool immune;
    private float immunityTimer;
    private bool stunned;
    private float stunTimer;
    private bool frozen;
    private float freezeTimer;

    [SerializeField] private AudioClip footsteps;

    private Collider2D thisCollider;
    private Rigidbody2D thisRigidbody;
    private SpriteRenderer sprite;

    private MyAnimator myAnimator;
    private string currentState;
    [SerializeField] private string defaultAnimState;
    [SerializeField] private string hurtAnimState;
    [SerializeField] private string idleAnimState;

    private float originalAlpha;
    private float stunAlpha;

    // [SerializeField] private AudioSource hurtSoundEffect;
    [SerializeField] private AudioClip hurtSound;


    private new void OnEnable()
    {
        base.OnEnable();
        UpdateDifficulty();
        freezeIconRenderer.enabled = false;
    }
    public override void OnOptionChanged()
    {
        UpdateDifficulty();
    }
    private void UpdateDifficulty()
    {
        immunitySeconds = immunitySecondsBase * OptionsManager.Instance.currentDifficulty.enemyImmunityFrameMultiplier;
        damageTakenOnHit = damageTakenOnHitBase * OptionsManager.Instance.currentDifficulty.enemyDamageMultiplier;
    }


    void Start()
    {
        thisCollider = GetComponent<Collider2D>();
        thisRigidbody = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();

        myAnimator = GetComponent<MyAnimator>();
        myAnimator.ChangeAnimationState(defaultAnimState);

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
            immunityTimer -= Time.deltaTime;
        }
        if (stunTimer > 0)
        {
            stunTimer -= Time.deltaTime;
        }
        else if (stunned)
        {
            StunOff();
        }
        if (freezeTimer > 0)
        {
            freezeTimer -= Time.deltaTime;
        }
        else if (frozen)
        {
            FreezeOff();
        }
    }

    public void GetHurt(Collider2D other)
    {
        if (!(immunityTimer <= 0 && other.gameObject.CompareTag("PlayerProjectile")))
        {
            return;
        }

        Debug.Log("Enemy hit by player projectile");

        immunityTimer = 0;
        SoundEffectHolder.instance.SoundEffect.clip = hurtSound;
        SoundEffectHolder.instance.SoundEffect.Play();

        immunityTimer = immunitySeconds;

        PlayerProjectile projectile = other.GetComponent<PlayerProjectile>();

        if (projectile.doesKill)
        {
            Die();
        }
        else
        {
            if (projectile.stunTime > 0)
            {
                Debug.Log("Stunning enemy", this);
                GetStunned(projectile.stunTime * OptionsManager.Instance.currentDifficulty.playerProjectileEffectDurationMultiplier);
            }

            if (projectile.freezeTime > 0)
            {
                Debug.Log("Freezing enemy", this);
                GetFrozen(projectile.freezeTime * OptionsManager.Instance.currentDifficulty.playerProjectileEffectDurationMultiplier);
            }

        }
    }

    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (immunityTimer <= 0 && other.gameObject.CompareTag("PlayerProjectile"))
    //     {
    //         Debug.Log("Enemy hit by player projectile");

    //         if (shield && shield.OverlapPoint(other.ClosestPoint(transform.position))) {
    //             Debug.Log("Shield was hit!");
    //             return;
    //         }

    //         immunityTimer = 0;
    //         SoundEffectHolder.instance.SoundEffect.clip = hurtSound;
    //         SoundEffectHolder.instance.SoundEffect.Play();

    //         immunityTimer = immunitySeconds;

    //         PlayerProjectile projectile = other.GetComponent<PlayerProjectile>();

    //         if (projectile.doesKill)
    //         {
    //             Die();
    //         }
    //         else
    //         {
    //             if (projectile.stunTime > 0)
    //             {
    //                 Debug.Log("Stunning enemy", this);
    //                 GetStunned(projectile.stunTime);
    //             }

    //             if (projectile.freezeTime > 0) {
    //                 Debug.Log("Freezing enemy", this);
    //                 GetFrozen(projectile.freezeTime);
    //             }
    //         }
    //     }
    // }

    void GetFrozen(float freezeTime, bool doShowIcon = true)
    {
        freezeTimer = freezeTime;
        sprite.color = Color.cyan;
        frozen = true;
        if (TryGetComponent<GuardMovement2>(out var guardMovement))
        {
            guardMovement.Freeze(freezeTime);
        }
        else
        {
            Debug.Log("No GuardMovement2 on this", gameObject);
        }
        if (TryGetComponent<JumpPeriodically>(out var jumpPeriodically))
        {
            jumpPeriodically.Freeze(freezeTime);
        }
        // StartCoroutine(UnFreeze(freezeTime));
        if (doShowIcon) {
            freezeIconRenderer.enabled = true;
        }
    }

    void FreezeOff()
    {
        Debug.Log("Unfreezing enemy", this);
        frozen = false;
        sprite.color = Color.white;
        freezeIconRenderer.enabled = false;
    }

    // IEnumerator UnFreeze(float freezeTime)
    // {
    //     yield return new WaitForSeconds(freezeTime);
    //     sprite.color = Color.white;
    //     freezeIconRenderer.enabled = false;
    // }

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
        Debug.Log("DYING!");
        sprite.flipY = true;
        GetFrozen(10000, false);
        Invoke(nameof(DestroySelf), 500); // 4294967295
        thisRigidbody.excludeLayers = LayerMask.GetMask("World");
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }

    public AudioClip getFootsteps()
    {
        return footsteps;
    }

}
