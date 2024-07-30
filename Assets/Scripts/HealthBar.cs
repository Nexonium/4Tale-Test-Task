using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{

    public Slider healthSlider;
    public Slider defenceSlider;

    public TextMeshProUGUI healthText;
    public TextMeshProUGUI maxHealthText;

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
        maxHealthText.text = value.ToString();
    }
    public void SetHealth(int currentHealth)
    {
        healthSlider.value = currentHealth;
        healthText.text = currentHealth.ToString();
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
