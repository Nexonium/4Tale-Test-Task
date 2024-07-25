
using UnityEngine;

public class PlayerEntity : Entity
{

    public int energy;
    public int maxEnergy;

    protected override void Die()
    {
        Debug.Log("Player has died!");
    }

    public void RestoreEnergy(int amount)
    {
        energy += amount;
    }
}
