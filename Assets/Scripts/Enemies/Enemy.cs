using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour, IOptionObserver
{    
    private float immunityTimer;
    private bool stunned; 
    // TODO some way to make sure that stun timer resets with each hit
    // TODO fix falling through floor on stun

    [SerializeField] private float damageTakenOnHitBase;
    private float damageTakenOnHit;
    [SerializeField] private float immunitySecondsBase;
    private float immunitySeconds;

    private Collider2D thisCollider;
    private SpriteRenderer sprite;
    
    private float originalAlpha;
    private float stunAlpha;

    [SerializeField] private AudioSource hurtSoundEffect;

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
        immunitySeconds = immunitySecondsBase * OptionsManager.Instance.currentDifficulty.enemyImmunityFrameMultiplier;
        damageTakenOnHit = damageTakenOnHitBase * OptionsManager.Instance.currentDifficulty.enemyDamageMultiplier;
    }


    void Start()
    {
        thisCollider = GetComponent<Collider2D>();
        sprite = GetComponent<SpriteRenderer>();
        originalAlpha = sprite.material.GetFloat("_Alpha");
        stunAlpha = originalAlpha * 0.5f;
        thisCollider.isTrigger = false; // Initially, collider acts as solid
        immunityTimer = 0;
    }

    void Update()
    {
        if (immunityTimer > 0)
        {
            immunityTimer-= Time.deltaTime;
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
        thisCollider.isTrigger = true;
        sprite.material.SetFloat("_Alpha", stunAlpha);
        StartCoroutine(StunOff(stunTime));
    }

    IEnumerator StunOff(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        sprite.material.SetFloat("_Alpha", originalAlpha);
        thisCollider.isTrigger = false;
    }

    void Die()
    {
        // TODO: Add death animation
        Debug.Log("DYING!");
        Destroy(gameObject);
    }
}
