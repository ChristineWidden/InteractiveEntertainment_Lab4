using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System;

public class PlayerProjectile : MonoBehaviour
{
    public Vector2 moveDirection;
    public PowerUpEnum powerUpType;

    [SerializeField, TextArea] private string description;


    [SerializeField] private float existTime;
    [SerializeField] private float moveSpeed;
    public float stunTime;
    public bool doesKill;
    public float freezeTime;

    private void OnEnable()
    {
        Invoke(nameof(Destroy), existTime);
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }

    void Update()
    {
        transform.Translate(moveSpeed * Time.deltaTime * moveDirection);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.Log(name + " collided with " + other);
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        // Debug.Log(name + " collided with " + other);
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }

    public string GetDescription() {
        return description;
    }
}