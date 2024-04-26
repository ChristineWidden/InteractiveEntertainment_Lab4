using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PowerUp : MonoBehaviour
{
    private float initY;
    private float time = 0.0f;
    [SerializeField] private float floatSpeed;
    [SerializeField] private float floatHeight;

    [SerializeField] public GameObject associatedProjectile;

    public float powerUpDuration;

    public PowerUpEnum powerUpType;

    public UnityEvent destroyedEvent;

    // Start is called before the first frame update
    void Start()
    {
        initY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        transform.position = new Vector2(transform.position.x, initY + Mathf.Sin(floatSpeed * time) * floatHeight);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            AudioSource boopSound = SoundEffectHolder.soundEffectInstance.COLLECT_POWER_UP;
            boopSound.Play();
            destroyedEvent.Invoke();
            Destroy(gameObject);
            // gameObject.SetActive(false);
            // Invoke(nameof(DestroyPowerUp), 0.5f);
        }
    }
}
