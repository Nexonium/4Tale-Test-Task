
using UnityEngine;

[CreateAssetMenu(fileName = "DamageEffect", menuName = "Card Effects/Damage")]
public class DamageEffect : TargetableEffect
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
        Debug.Log($"Dealing {effectValue} damage to {target.name}");
        
        target.TakeDamage(effectValue);
    }

}
