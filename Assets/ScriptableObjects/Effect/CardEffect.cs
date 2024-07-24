using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CardEffect : ScriptableObject
{
    public int EffectValue;
    public bool IsTargetable;
    public abstract void ApplyEffect(GameObject target);
}
