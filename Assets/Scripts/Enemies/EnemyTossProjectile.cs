using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Random;

public class EnemyTossProjectile : MonoBehaviour
{
    [SerializeField] private float startAngle;
    [SerializeField] private float initVelocity;
    // [SerializeField] private AudioClip throwSound;

    public float fireDirection;

    [SerializeField]
    private static System.Random random = new System.Random();

    [SerializeField] private float fireInterval;

    [SerializeField] private GameObject projectile;

    // Start is called before the first frame update
    void Start()
    {
        // TODO have it go off when player in proximity
        InvokeRepeating(nameof(Fire), 0f, fireInterval);
        //Invoke("Fire", 2f);
    }

    private void Fire() {
        // SoundEffectHolder.instance.SoundEffect.clip = throwSound;
        // SoundEffectHolder.instance.SoundEffect.Play();

        Vector2 arrowDirection = initVelocity * (new Vector2(fireDirection * Mathf.Cos(startAngle), Mathf.Sin(startAngle))).normalized;

        GameObject thisProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
        // projectile.transform.position = transform.position;
        // Rigidbody2D rigidbody = thisProjectile.GetComponent<Rigidbody2D>();
        Physics physics = thisProjectile.GetComponent<Physics>();
        physics.velocity = arrowDirection;
        // rigidbody.velocity = arrowDirection;

        thisProjectile.transform.parent = ProjectileHolder.instance.transform;
    
    }
}
