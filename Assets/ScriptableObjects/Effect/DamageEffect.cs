using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DamageEffect", menuName = "Card Effects/Damage")]
public class DamageEffect : CardEffect
{

    public override void ApplyEffect(GameObject target)
    {
        Debug.Log($"Dealing {EffectValue} damage to {target.name}");
    }
}
