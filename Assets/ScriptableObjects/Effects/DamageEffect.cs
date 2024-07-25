
using UnityEngine;

[CreateAssetMenu(fileName = "DamageEffect", menuName = "Card Effects/Damage")]
public class DamageEffect : TargetableEffect
{

    public override void ApplyEffect(GameObject target)
    {
        if (target == null) { target = defaultTarget; }
        Debug.Log($"Dealing {effectValue} damage to {target.name}");
    }

}
