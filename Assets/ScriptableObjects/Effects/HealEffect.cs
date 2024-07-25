using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealEffect", menuName = "Card Effects/Heal")]
public class HealEffect : CardEffect
{

    public override void ApplyEffect(GameObject target)
    {
        Debug.Log($"Healing {effectValue} health to {target.name}");
    }
}

