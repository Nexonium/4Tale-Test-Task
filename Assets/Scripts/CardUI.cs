using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardUI : MonoBehaviour
{

    public Card card;

    public TMP_Text cardName;
    public TMP_Text cardDescription;
    public TMP_Text cardCost;
    public Image cardArt;

    //public void SetCard(Card newCard)
    //{
    //    card = newCard;
    //    cardName.text = card.cardName;
    //    cardDescription.text = card.cardDescription;
    //    cardArt.sprite = card.cardArt;
    //}

    // Refresh object in inspector/editor window
    private void OnValidate()
    {
        Awake();
    }

    private void Awake()
    {
        //card = GetComponent<Card>();
        SetCardUI();
    }

    public void SetCardUI()
    {
        if (card != null && card != null)
        {
            SetCardTexts();
            SetCardImages();
        }
    }

    private void SetCardTexts()
    {
        cardName.text = card.cardName;
        cardDescription.text = card.cardDescription;
        cardCost.text = card.cardCost.ToString();
    }

    private void SetCardImages()
    {
        cardArt.sprite = card.cardArt;
    }
}
