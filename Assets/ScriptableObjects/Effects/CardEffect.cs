
using UnityEngine;

public abstract class CardEffect : ScriptableObject
{

    public int effectValue;
    public GameObject defaultTarget;

    public abstract void ApplyEffect(GameObject target);
}

public enum EffectTarget
{
    Player,
    Enemy
}
