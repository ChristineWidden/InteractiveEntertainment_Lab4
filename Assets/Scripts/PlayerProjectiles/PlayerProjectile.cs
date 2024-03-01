using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System;

public class PlayerProjectile : MonoBehaviour
{
    public Vector2 moveDirection;
    [SerializeField] private float existTime;
    [SerializeField] private float moveSpeed;

    private void OnEnable() {
        Invoke("Destroy", existTime);
    }

    private void Destroy() {
        Destroy(gameObject);
    }

    void Update() {
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("The arrow has hit!");
    }
}