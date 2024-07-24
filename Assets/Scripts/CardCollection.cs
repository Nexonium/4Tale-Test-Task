
using System.Collections.Generic;
using UnityEngine;

public class CardCollection : MonoBehaviour
{

    public List<Card> CardsInCollection { get; private set; }

    public void RemoveCardFromCollection(Card card)
    {
        if (CardsInCollection.Contains(card))
        {
            CardsInCollection.Remove(card);
        }
        else
        {
            Debug.LogWarning(card.cardName + "is not in card collection!");
        }
    }

}