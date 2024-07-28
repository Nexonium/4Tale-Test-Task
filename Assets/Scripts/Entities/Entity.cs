
using UnityEngine;

/// <summary>
/// Basic settings and stats of entities. Health, defence, death, and other methods.
/// </summary>

public class Entity : MonoBehaviour
{

    public int health;
    public int maxHealth;

    public int defence;

    public void TakeDamage(int damage)
    {
        int damageAfterDefence = Mathf.Max(damage - defence, 0);
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

    protected virtual void Die()
    {
        //TODO: Game over or win scene/next scene

        Destroy(gameObject);
    }
}
