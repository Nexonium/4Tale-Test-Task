using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandFieldController : MonoBehaviour
{

    public GameObject cardPrefab;
    public RectTransform handPanel;
    public float cardSpacing = 30f;
    public float maxRotationAngle = 20;

    public void UpdateHandDisplay(Card[] hand)
    {
        foreach (Transform child in handPanel)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < hand.Length; i++)
        {
            GameObject cardObject = Instantiate(cardPrefab, handPanel);
            RectTransform cardRect = cardObject.GetComponent<RectTransform>();

            float cardIndex = i - (hand.Length - 1) / 2.0f;
            float angle = cardIndex * (maxRotationAngle / (hand.Length - 1));
            float xOffset = cardIndex * cardSpacing;

            cardRect.anchoredPosition = new Vector2(xOffset, 0);
            cardRect.localRotation = Quaternion.Euler(0, 0, -angle);

            CardUI cardUI = cardObject.GetComponent<CardUI>();
            cardUI.SetCard(hand[i]);

        }
    }
}
