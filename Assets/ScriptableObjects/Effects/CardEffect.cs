
using UnityEngine;

public abstract class CardEffect : ScriptableObject
{

    public int effectValue;
    public Entity defaultTarget;

    public abstract void ApplyEffect(Entity target = null);
}

public enum EffectTarget
{
    Player,
    Enemy
}
