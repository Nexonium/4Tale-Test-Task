using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DefenceEffect", menuName = "Card Effects/Defence")]
public class DefenceEffect : CardEffect
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
        Debug.Log($"Gaining {effectValue} block to {target.name}");

        target.GainDefence(effectValue);
    }
}

