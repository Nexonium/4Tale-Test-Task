
using UnityEngine;

/// <summary>
/// Base class for targetable effects. Target is enemy by default
/// </summary>

public class TargetableEffect : CardEffect
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
        Debug.Log($"Targeting {target.name}!");
    }

    public void SelectTarget(Entity[] potentialTargets)
    {
        // Selection logic
    }
}

