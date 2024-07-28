using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

/// <summary>
/// Sets and manages properties of the CardPrefab, drag, selection, and other properties
/// </summary>

public class CardUI : MonoBehaviour, IPointerClickHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler, IDragHandler
{
    #region Variables

    [Header("Prefab Elements")]
    public Card card;
    public TMP_Text cardName;
    public TMP_Text cardDescription;
    public TMP_Text cardCost;
    public Image cardArt;

    [Header("Selector Properties")]
    public float scaleFactor = 1.2f;

    private bool isDragging;
    private bool isTargeting;
    private bool isHighlighted;

    public bool isTargetable;

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

    public void PlayCard(Entity target)
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

    public void OnPointerUp(PointerEventData eventData)
    {
        if (isDragging)
        {
            transform.position = originalPosition;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDragging)
        {
            transform.position = eventData.position;
        }
        else if (!isDragging && isTargeting)
        {
            StartTargeting();
        }
    }

    #endregion

    #region Private Methods

    private void StartDragging()
    {
        isDragging = true;
        originalPosition = transform.position;
    }

    private void StartTargeting()
    {
        isTargeting = true;
        arrow.enabled = true;
    }

    private void HandleLeftClick()
    {
        if (HasEnoughEnergy())
        {
            if (card.HasTargetableEffect())
            {
                if (isTargeting)
                {
                    if (TryGetTarget(out Entity target))
                    {
                        PlayCard(target);
                    }
                }
                else
                {
                    StartTargeting();
                }
            }
            else
            {
                StartDragging();
            }
        }
    }

    private void HandleRightClick()
    {
        if (isTargeting)
        {
            CancelSelection();
        }
    }

    private bool TryGetTarget(out Entity target)
    {
        target = null;
        // TODO: Find EnemyEntity
        return target != null;
    }

    private void HighlightCard()
    {
        isHighlighted = true;
        originalRotation = rectTransform.localRotation;
        originalSiblingIndex = rectTransform.GetSiblingIndex();

        rectTransform.localRotation = Quaternion.identity;
        rectTransform.localScale = Vector3.one * scaleFactor;
        rectTransform.SetSiblingIndex(rectTransform.parent.childCount - 1);
    }

    private void CancelHighlight()
    {
        isHighlighted = false;
        rectTransform.localRotation = originalRotation;
        rectTransform.SetSiblingIndex(originalSiblingIndex);
        rectTransform.localScale = Vector3.one;
    }

    private void CancelSelection()
    {
        isTargeting = false;
        arrow.enabled = false;
        CancelHighlight();
    }

    #endregion

    #region Init Methods



    #endregion
}