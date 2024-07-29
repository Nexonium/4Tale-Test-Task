
using UnityEngine;

/// <summary>
/// Settings for player entity. Health, defence, energy, and other methods
/// </summary>

public class PlayerEntity : Entity
{

    public int maxEnergy;
    public int energy;
    public int handSize;

    public override void Initialize()
    {
        healthBar.Initialize(maxHealth, health);

        // Subscribing healthbar to the events
        OnHealthChanged += healthBar.SetHealth;
        OnDefenceChanged += healthBar.SetDefence;

        RestoreEnergy(maxEnergy);
    }

    public void RestoreEnergy(int amount)
    {
        energy = Mathf.Min(energy + amount, maxEnergy);
    }

    public bool CanPlayCard(Card card)
    {
        return energy >= card.cardCost;
    }

    public void PlayCard(Card card, Entity target)
    {
        energy -= card.cardCost;
        card.PlayEffects(target);
    }

    public bool HasEnoughEnergy(Card card)
    {
        return energy >= card.cardCost;
    }

    protected override void Die()
    {
        Debug.Log("Player has died!");

        Destroy(gameObject);
    }


}
