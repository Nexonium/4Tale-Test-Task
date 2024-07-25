
using UnityEngine;

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

    protected virtual void Die()
    {

    }
}
