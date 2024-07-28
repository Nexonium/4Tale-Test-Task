
using UnityEngine;

[CreateAssetMenu(fileName = "DamageEffect", menuName = "Card Effects/Damage")]
public class DamageEffect : TargetableEffect
{

    private void Awake()
    {
        CacheReferences();
    }

    private void CacheReferences()
    {
        defaultTarget = FindObjectOfType<EnemyEntity>();
    }

    public override void ApplyEffect(Entity target)
    {
        if (target == null) { target = defaultTarget; }
        Debug.Log($"Dealing {effectValue} damage to {target.name}");
        
        target.TakeDamage(effectValue);
    }

}
