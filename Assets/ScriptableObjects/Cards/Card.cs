
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCard", menuName = "Card")]
public class Card : ScriptableObject
{

    public string cardName;
    public string cardDescription;
    public int cardCost;
    public Sprite cardArt;

    public TargetableEffect targetableEffect;
    public List<CardEffect> cardEffects = new();

    public bool HasTargetableEffect()
    {
        return targetableEffect != null;
    }

    public void PlayEffects(Entity target = null)
    {
        if (HasTargetableEffect())
        {
            targetableEffect.ApplyEffect(target);
        }

        foreach (CardEffect effect in cardEffects)
        {
            effect.ApplyEffect();
        }
    }
}
