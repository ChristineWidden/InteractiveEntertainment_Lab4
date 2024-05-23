using UnityEngine;

public class GuardDamage : IOptionObserver
{

    [SerializeField] private float currentHealth = 100;


    [SerializeField] private float damageTakenOnHitBase;
    private float damageTakenOnHit;
    [SerializeField] private float immunitySecondsBase;
    public float immunitySeconds;
    private float immunityTimer;

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

    // Start is called before the first frame update
    void Start()
    {
        immunityTimer = 0;
    }

    // Update is called once per frame
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
            currentHealth -= damageTakenOnHit;
            if (currentHealth < 1)
            {
                // Debug.Log("GUARD IS DEAD!!!!!");

                Destroy(gameObject);
            }
        }
    }
}
