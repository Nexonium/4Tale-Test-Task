
using UnityEngine;

[CreateAssetMenu(fileName = "HealEffect", menuName = "Card Effects/Heal")]
public class HealEffect : CardEffect
{
    public override void Initialize()
    {
        CacheReferences();
    }

    private void CacheReferences()
    {
        if (effectTarget == EffectTarget.Player)
        {
            defaultTarget = FindObjectOfType<PlayerEntity>();
        }
        else
        {
            defaultTarget = FindObjectOfType<EnemyEntity>();
        }
    }

    public override void ApplyEffect(Entity target = null)
    {
        if (target == null) { target = defaultTarget; }
        Debug.Log($"Healing {effectValue} health to {target.name}");

        target.GainHealth(effectValue);
    }
}

