using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthRing : IOptionObserver
{
    [SerializeField] private Sprite[] sprites;
    SpriteRenderer spriteRenderer;
    private int currentHealth;
    [SerializeField] private int maxHealth;
    [SerializeField] private bool isSmallHealth;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        SetHealth(maxHealth);
        OnOptionChanged();
    }

    public void SetHealth(int health)
    {
        // Check if index is within bounds of the sprites array
        if (health >= 0 && health < sprites.Length)
        {
            // Set the sprite
            spriteRenderer.sprite = sprites[health];
            currentHealth = health;
        }
        else
        {
            Debug.LogWarning("Index out of bounds for sprites array");
        }
    }

    public override void OnOptionChanged()
    {
        Debug.Log("SMALL HEALTH CHANGED " + OptionsManager.Instance.GetBooleanOption(BooleanOptionEnum.SMALL_HEALTH_RING));
        if (OptionsManager.Instance.GetBooleanOption(BooleanOptionEnum.SMALL_HEALTH_RING)) {
            spriteRenderer.enabled = isSmallHealth;
        } else {
            spriteRenderer.enabled = !isSmallHealth;
        }
    }
}
