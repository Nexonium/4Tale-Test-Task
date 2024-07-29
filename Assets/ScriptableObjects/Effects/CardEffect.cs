
using UnityEngine;

/// <summary>
/// Abstract base class for card effects
/// </summary>

public abstract class CardEffect : ScriptableObject
{

    public int effectValue;
    public EffectTarget effectTarget;
    public Entity defaultTarget;

    public abstract void ApplyEffect(Entity target = null);
    public abstract void Initialize();
}

public enum EffectTarget
{
    Player,
    Enemy
}
