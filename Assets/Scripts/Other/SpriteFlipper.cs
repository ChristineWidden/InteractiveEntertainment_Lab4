using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System;


public class SpriteFlipper : MonoBehaviour
{
    [SerializeField] private SpriteRenderer parentRenderer;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update() {
        spriteRenderer.flipX = parentRenderer.flipX;
    }
}