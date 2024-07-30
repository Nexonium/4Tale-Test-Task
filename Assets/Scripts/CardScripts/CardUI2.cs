using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

/// <summary>
/// Sets and manages properties of the CardPrefab, drag, selection, and other properties
/// </summary>

public class CardUI2 : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    #region Variables

    [Header("Prefab elements")]
    public Card card;
    public TMP_Text cardName;
    public TMP_Text cardDescription;
    public TMP_Text cardCost;
    public Image cardArt;

    [Header("Highlight/Select properties")]
    public float scaleFactor = 1.2f;

    private bool isTargeting;

    private ArrowArcRenderer arrow;
    private RectTransform rectTransform;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private int originalSiblingIndex;

    private GameManager gameManager;

    #endregion

    #region Unity Methods

    private void Awake()
    {
        InitializeComponents();
    }

    private void Start()
    {
        SetCard(card);
    }

    private void InitializeComponents()
    {
        rectTransform = GetComponent<RectTransform>();
        arrow = GetComponent<ArrowArcRenderer>();
        gameManager = FindObjectOfType<GameManager>();
    }

    #endregion

    #region Card Methods

    public void SetCard(Card card)
    {
        if (card != null)
        {
            this.card = card;
            card.Initialize();
            UpdateCardUI();
        }
    }

    private void UpdateCardUI()
    {
        cardName.text = card.cardName;
        cardDescription.text = card.cardDescription;
        cardCost.text = card.cardCost.ToString();
        cardArt.sprite = card.cardArt;
    }

    public bool HasEnoughEnergy()
    {
        return gameManager.player.HasEnoughEnergy(card);
    }

    public bool HasTargetableEffect()
    {
        return card.HasTargetableEffect();
    }

    public bool isSelectedTarget()
    {
        return gameManager.isTargetedEntity(card.targetableEffect.defaultTarget);
    }

    public bool isOutOfHand()
    {
        return gameManager.isOutOfHand();
    }

    public void PlayCard(Entity target = null)
    {
        gameManager.PlayCard(card, target);
        Destroy(gameObject);
    }

    #endregion

    #region Event Handlers

    public void OnPointerEnter(PointerEventData eventData)
    {
        HighlightCard();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isTargeting)
        {
            CancelHighlight();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        switch (eventData.button)
        {
            case PointerEventData.InputButton.Left:
                HandleLeftClick();
                break;
            case PointerEventData.InputButton.Right:
                HandleRightClick();
                break;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalPosition = transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (HasEnoughEnergy())
        {
            if (!HasTargetableEffect())
            {
                transform.position = eventData.position;
            }
            else if (!isTargeting)
            {
                StartTargeting();
            }
        }
        else
        {
            gameManager.ShowPlayerMessage();
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!HasTargetableEffect() && HasEnoughEnergy())
        {
            if (isOutOfHand())
            {
                PlayCard();
            }
            else
            {
                transform.position = originalPosition;
            }
        }
    }

    #endregion

    #region Private Methods

    private void StartTargeting()
    {
        isTargeting = true;
        arrow.enabled = true;
    }

    private void HandleLeftClick()
    {
        if (card.HasTargetableEffect())
        {
            if (HasEnoughEnergy())
            {
                if (!isTargeting)
                {
                    StartTargeting();
                }
                else
                {
                    if (isSelectedTarget())
                    {
                        PlayCard();
                    }
                }
            }
            else
            {
                gameManager.ShowPlayerMessage();
            }
        }
    }

    private void HandleRightClick()
    {
        if (isTargeting)
        {
            CancelTargeting();
        }
    }

    private void HighlightCard()
    {
        originalPosition = rectTransform.localPosition;
        originalRotation = rectTransform.localRotation;
        originalSiblingIndex = rectTransform.GetSiblingIndex();

        rectTransform.localRotation = Quaternion.identity;
        rectTransform.localScale = Vector3.one * scaleFactor;
        rectTransform.SetSiblingIndex(rectTransform.parent.childCount - 1);
    }

    private void CancelHighlight()
    {
        rectTransform.localRotation = originalRotation;
        rectTransform.SetSiblingIndex(originalSiblingIndex);
        rectTransform.localScale = Vector3.one;
    }

    private void CancelTargeting()
    {
        isTargeting = false;
        arrow.enabled = false;
        CancelHighlight();
    }

    #endregion

}
