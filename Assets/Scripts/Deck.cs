using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public static Deck Instance { get; private set; }   // Singleton

    public List<Card> deckPile = new();
    public List<Card> handPile = new();
    public List<Card> discardPile = new();

    public int handSize = 5;

    public HandFieldController handFieldController;

    // Singleton method
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        ShuffleDeck();
        DrawCard();
        handFieldController.UpdateHandDisplay(handPile.ToArray());
    }

    // Shuffling using Fisher–Yates shuffle
    private void ShuffleDeck()
    {
        for (int i = deckPile.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            var temp = deckPile[i];
            deckPile[i] = deckPile[j];
            deckPile[j] = temp;
        }
    }

    public void DrawCard(int amount = 5)
    {
        for (int i = 0; i < amount; i++)
        {

            // If deck is empty, we put discarded cards into the deck and shuffle
            if (deckPile.Count <= 0)
            {
                discardPile = deckPile;
                discardPile.Clear();
                ShuffleDeck();
            }

            // Drawing top card from the deck to the hand
            handPile.Add(deckPile[0]);
            deckPile.RemoveAt(0);
        }
    }

    // This method discards only card from hand
    // If you needed to discard card from deck, you need to create new method
    public void DiscardCard(Card card)
    {
        if (handPile.Contains(card))
        {
            handPile.Remove(card);
            discardPile.Add(card);
            //card.gameObject.SetActive(false);
        }
    }
}
