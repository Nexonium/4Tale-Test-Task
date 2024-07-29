
using UnityEngine;

[CreateAssetMenu(fileName = "DefenceEffect", menuName = "Card Effects/Defence")]
public class DefenceEffect : CardEffect
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
        Debug.Log($"Gaining {effectValue} block to {target.name}");

        target.GainDefence(effectValue);
    }
}

