
using System;
using UnityEngine;

/// <summary>
/// Basic settings and stats of entities. Health, defence, death, and other methods.
/// </summary>

public class Entity : MonoBehaviour
{

    public int _health;
    public int maxHealth;

    public int _defence;

    public HealthBar healthBar;

    public event Action<int> OnHealthChanged;
    public event Action<int> OnDefenceChanged;

    // Declaring event variables to subscribe
    public int health
    {
        get { return _health; }
        set
        {
            if (_health != value)
            {
                _health = value;
                OnHealthChanged?.Invoke(health);
            }
        }
    }

    public int defence
    {
        get { return _defence; }
        set
        {
            if (_defence != value)
            {
                _defence = value;
                OnDefenceChanged?.Invoke(defence);
            }
        }
    }

    public virtual void Initialize()
    {
        healthBar.Initialize(maxHealth, _health);
        
        // Subscribing healthbar to the events
        OnHealthChanged += healthBar.SetHealth;
        OnDefenceChanged += healthBar.SetDefence;
    }

    public void TakeDamage(int damage)
    {
        int damageAfterDefence = Mathf.Max(damage - defence, 0);
        defence = Mathf.Max(0, defence - damage);
        health -= damageAfterDefence;
        if (health <= 0)
        {
            Die();
        }
    }

    public void GainDefence(int amount)
    {
        defence += amount;
    }

    public void GainHealth(int amount)
    {
        if (health < maxHealth)
        {
            int healthAfterHeal = Mathf.Min(health + amount, maxHealth);
            health = healthAfterHeal;
        }
    }

    public void ResetDefence()
    {
        defence = 0;
    }

    protected virtual void Die()
    {
        //TODO: Game over or win scene/next scene

        Destroy(gameObject);
    }
}
