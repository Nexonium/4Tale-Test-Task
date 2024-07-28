using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealEffect", menuName = "Card Effects/Heal")]
public class HealEffect : CardEffect
{
    private void Awake()
    {
        CacheReferences();
    }

    private void CacheReferences()
    {
        defaultTarget = FindObjectOfType<PlayerEntity>();
    }

    public override void ApplyEffect(Entity target)
    {
        if (target == null) { target = defaultTarget; }
        Debug.Log($"Healing {effectValue} health to {target.name}");

        target.GainHealth(effectValue);
    }
}

