
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{

    public GameObject cardPrefab;
    public Transform handPanel;
    public TextMeshProUGUI energyText;

    public float cardSpacing = 30f;
    public float maxRotationAngle = 20;

    public void UpdateEnergy(int energy)
    {
        energyText.text = energy.ToString();
    }

    public void UpdateHand(List<Card> hand)
    {

        // Cleaning cards view and creating new
        foreach (Transform child in handPanel)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < hand.Count; i++)
        {
            GameObject cardObject = Instantiate(cardPrefab, handPanel);
            RectTransform cardRect = cardObject.GetComponent<RectTransform>();

            float cardIndex = i - (hand.Count - 1) / 2.0f;
            float angle = cardIndex * (maxRotationAngle / (hand.Count - 1));
            float xOffset = cardIndex * cardSpacing;

            cardRect.anchoredPosition = new Vector2(xOffset, 0);
            cardRect.localRotation = Quaternion.Euler(0, 0, -angle);

            CardUI cardUI = cardObject.GetComponent<CardUI>();
            cardUI.SetCard(hand[i]);
        }
    }
}