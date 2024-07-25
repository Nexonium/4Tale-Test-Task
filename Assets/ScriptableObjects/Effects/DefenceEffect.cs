using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DefenceEffect", menuName = "Card Effects/Defence")]
public class DefenceEffect : CardEffect
{

    public override void ApplyEffect(GameObject target)
    {
        Debug.Log($"Gaining {effectValue} block to {target.name}");
    }
}

