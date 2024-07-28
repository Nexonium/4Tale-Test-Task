
using UnityEngine;

/// <summary>
/// Settings for targetable effects
/// </summary>

public class TargetableEffect : CardEffect
{

    public override void ApplyEffect(Entity target = null)
    {
        if (target == null) { target = defaultTarget; }
        Debug.Log($"Targeting {target.name}!");
    }

    public void SelectTarget(Entity[] potentialTargets)
    {
        // Selection logic
        // For example, bezier curve arrow
    }
}

