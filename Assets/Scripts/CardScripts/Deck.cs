using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Settings for deck, hand, and discard pile
/// </summary>

public class Deck : MonoBehaviour
{
    public static Deck Instance { get; private set; }   // Singleton

    public List<Card> drawPile = new();
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
        //Initialize();
    }

    public void Initialize()
    {
        ShuffleDeck(drawPile);
        //DrawHand();
        handFieldController.UpdateHandDisplay(handPile.ToArray());
    }

    // Shuffling cards using Fisher–Yates shuffle
    private void ShuffleDeck(List<Card> deckPile)
    {
        for (int i = deckPile.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            var temp = deckPile[i];
            deckPile[i] = deckPile[j];
            deckPile[j] = temp;
        }
    }

    public void DrawCards(int amount)
    {
        for (int i = 0; i < amount; i++)
        {

            // If deck is empty, we put discarded cards into the deck and shuffle
            if (drawPile.Count <= 0)
            {
                ShuffleDiscardToDraw();
            }

            // Drawing top card from the deck to the hand
            handPile.Add(drawPile[0]);
            drawPile.RemoveAt(0);
        }
    }

    public void ShuffleDiscardToDraw()
    {
        drawPile.AddRange(discardPile);
        discardPile.Clear();
        ShuffleDeck(drawPile);
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

    public void DrawHand()
    {
        DrawCards(handSize);
    }

    public void DiscardHand()
    {
        discardPile.AddRange(handPile);
        handPile.Clear();
    }
}
