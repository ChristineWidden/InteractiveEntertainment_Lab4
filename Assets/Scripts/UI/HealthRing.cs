using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthRing : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;
    SpriteRenderer spriteRenderer;
    private int currentHealth;
    [SerializeField] private int maxHealth;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        SetHealth(maxHealth);
    }

    public void SetHealth(int health)
    {
        // Check if index is within bounds of the sprites array
        if (health >= 0 && health < sprites.Length)
        {
            // Set the sprite
            spriteRenderer.sprite = sprites[maxHealth + 1 - health];
            currentHealth = health;
        }
        else
        {
            Debug.LogWarning("Index out of bounds for sprites array");
        }
    }

}
