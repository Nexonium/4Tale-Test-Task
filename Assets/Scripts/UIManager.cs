
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{

    [Header("Card, energy, and hand panel")]
    public GameObject cardPrefab;
    public Transform handPanel;
    public TextMeshProUGUI energyText;
    public TextMeshProUGUI endBattleText;
    public GameObject playerSpeech;

    public float cardSpacing = 30f;
    public float maxRotationAngle = 20;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        endBattleText.enabled = false;
    }

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

            CardUI2 cardUI = cardObject.GetComponent<CardUI2>();
            cardUI.SetCard(hand[i]);
        }
    }

    public bool isTargetedEntity(Entity entity)
    {
        RectTransform rect = entity.GetComponent<RectTransform>();
        return RectTransformUtility.RectangleContainsScreenPoint(rect, Input.mousePosition);
    }

    public bool isOutOfHand()
    {
        RectTransform rect = handPanel.GetComponent<RectTransform>();
        var result = !RectTransformUtility.RectangleContainsScreenPoint(rect, Input.mousePosition);
        return result;
    }

    public void ShowPlayerMessage()
    {
        if (!playerSpeech.active)
        {
            playerSpeech.SetActive(true);
            Invoke("HidePlayerMessage", 3.0f);
        }
    }

    public void HidePlayerMessage()
    {
        playerSpeech.SetActive(false);
    }

    public void ShowEndBattleText(string text)
    {
        endBattleText.enabled = true;
        endBattleText.text = text;
    }
}
