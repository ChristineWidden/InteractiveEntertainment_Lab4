using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar1 : MonoBehaviour
{
    [SerializeField] private GameObject healthPipPrefab;
    [SerializeField] private GameObject[] healthPips;
    public int maxHealth;
    [SerializeField] private int currentHealth;
    [SerializeField] private float totalWidth;

    [SerializeField] private int healthScale;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void SetHealth(int health){
        currentHealth = health;
        UpdateHealthDisplay();
    }

    void UpdateHealthDisplay()
    {
        for (int i = 0; i < healthPips.Length; i++)
        {
            if (i < currentHealth / healthScale)
            {
                healthPips[i].SetActive(true);
            }
            else
            {
                healthPips[i].SetActive(false);
            }
        }
    }

    public void GenerateHealthBar()
    {
        int adjustedMaxHealth = (int) maxHealth / healthScale;

        float pipWidth = healthPipPrefab.GetComponent<RectTransform>().sizeDelta.x;
        float spacing = totalWidth / (adjustedMaxHealth - 1 + adjustedMaxHealth * pipWidth / totalWidth);

        healthPips = new  GameObject[adjustedMaxHealth];

        for (int i = 0; i < adjustedMaxHealth; i++)
        {
            GameObject pip = Instantiate(healthPipPrefab, transform);
            healthPips[i] = pip;
            RectTransform pipRectTransform = pip.GetComponent<RectTransform>();
            pipRectTransform.anchoredPosition = new Vector2(i * (spacing + pipWidth), 0f);
        }
    }

}
