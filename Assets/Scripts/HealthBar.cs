using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Slider healthSlider;
    public Slider defenceSlider;

    public void Start()
    {
        CacheReferences();
    }

    private void CacheReferences()
    {
        
    }

    public void Initialize(int maxHealth, int currentHealth)
    {
        SetMaxHealth(maxHealth);
        SetHealth(currentHealth);

        SetMaxDefence(maxHealth);
        SetDefence(0);
    }

    public void SetMaxHealth(int value)
    {
        healthSlider.minValue = 0;
        healthSlider.maxValue = value;
    }
    public void SetHealth(int currentHealth)
    {
        healthSlider.value = currentHealth;
    }

    public void SetMaxDefence(int value)
    {
        defenceSlider.minValue = 0;
        defenceSlider.maxValue = value;
    }
    public void SetDefence(int amount)
    {
        defenceSlider.value = amount;
    }

}
