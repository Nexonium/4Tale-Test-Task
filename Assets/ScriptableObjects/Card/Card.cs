
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCard", menuName = "Card")]
public class Card : ScriptableObject
{

    public string cardName;
    public string cardDescription;
    public int cardCost;
    public Sprite cardArt;

    public List<CardEffect> cardEffects = new();
}
