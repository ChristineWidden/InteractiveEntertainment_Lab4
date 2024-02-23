using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthMeterScript : MonoBehaviour
{
    [SerializeField] private Material healthBarMaterial;
    public int maxHealth;
    private int currentHealth;

    public void SetMaxHealth(int health)
    {
        healthBarMaterial.SetFloat("_SegmentCount", (float) health);
        maxHealth = health;
        currentHealth = health;
    }

    public void SetHealth(int health)
    {
        healthBarMaterial.SetFloat("_RemovedSegments", (float) maxHealth - health);
        currentHealth = health;
    }

    void OnDestroy()
    {
        SetHealth(maxHealth);
    }
}
