using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

/// <summary>
/// Sets and manages properties of the CardPrefab, card drag, selection, and other properties
/// </summary>

public class CardUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    #region Variables

    [Header("Prefab Elements")]
    public Card card;

    public TMP_Text cardName;
    public TMP_Text cardDescription;
    public TMP_Text cardCost;
    public Image cardArt;


    [Header("Selector Properties")]
    public bool isDragging = false;
    public bool isTargeting = false;

    private BezierArrow bezierArrow;
    private GameObject playField;

    private RectTransform rectTransform;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private int originalSiblingIndex;

    public float scaleFactor = 1.2f;
    //public Vector3 hoverOffset = new(0, 20, 0);

    [Header("Drag Properties")]
    public bool isDraggable = false;


    #endregion

    #region Methods and Classes
    private void Start()
    {
        //bezierArrow = GetComponent<BezierArrow>;
        rectTransform = GetComponent<RectTransform>();
    }

    public void SetCard(Card card)
    {
        this.card = card;
        cardName.text = card.cardName;
        cardDescription.text = card.cardDescription;
        cardCost.text = card.cardCost.ToString();
        cardArt.sprite = card.cardArt;
    }

    public void ApplyEffects(GameObject target)
    {
        foreach (var effect in card.cardEffects)
        {
            effect.ApplyEffect(target);
        }
    }

    #region Selector and Drag methods

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Saving original properties
        //originalPosition = rectTransform.position;
        originalRotation = rectTransform.localRotation;
        originalSiblingIndex = rectTransform.GetSiblingIndex();

    // Setting new properties
        //rectTransform.position = hoverOffset;
        rectTransform.localRotation = Quaternion.Euler(0, 0, 0);
        rectTransform.localScale = Vector3.one * scaleFactor;
        rectTransform.SetSiblingIndex(rectTransform.parent.childCount - 1);

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Returning original properties
        //rectTransform.position = originalPosition;
        rectTransform.localRotation = originalRotation;
        rectTransform.localScale = Vector3.one;
        rectTransform.SetSiblingIndex(originalSiblingIndex);

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (card.HasTargetableEffect())
        {
            isTargeting = true;
            bezierArrow.Activate(transform.position, eventData.position);
            //HiglightCard();
        }
        else
        {
            isDragging = true;
            originalPosition = transform.position;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isTargeting)
        {
            bezierArrow.UpdateEndPoint(eventData.position);
        }
        else if (isDragging)
        {
            transform.position = eventData.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {

        if (isTargeting)
        {
            GameObject target = eventData.pointerCurrentRaycast.gameObject;
            if (target != null)
            {
                //ApplyEffects(target.GetComponent<Enemy>)
            }
            else
            {
                CancelSelection();
            }
        }
        else if (isDragging)
        {

            playField = FindAnyObjectByType<PlayField>().gameObject;
            if (playField != null)
            {

                RectTransform playFieldRect = playField.GetComponent<RectTransform>();
                if (RectTransformUtility.RectangleContainsScreenPoint(playFieldRect, Input.mousePosition))
                {
                    // TODO: Play card effect
                    Destroy(gameObject);

                    // Update hand
                    GameObject deckObject = FindAnyObjectByType<Deck>().gameObject;
                    Deck deck = deckObject.GetComponent<Deck>();
                    deck.DiscardCard(card);

                    GameObject handField = FindAnyObjectByType<HandFieldController>().gameObject;
                    HandFieldController handFieldController = handField.GetComponent<HandFieldController>();
                    handFieldController.UpdateHandDisplay(deck.handPile.ToArray());
                }
            }

            transform.position = originalPosition;
        }
    }

    private void CancelSelection()
    {
        isTargeting = false;
        bezierArrow.Deactivate();
        ResetCard();
    }

    private void ResetCard()
    {
        isDragging = false;
        isTargeting = false;
        bezierArrow.Deactivate();
        transform.position = originalPosition;
        transform.rotation = originalRotation;
        transform.localScale = Vector3.one;
    }
    #endregion

    #region Init object/prefab methods

    // Refreshing object in inspector/editor
    private void OnValidate()
        {
            Awake();
        }

        private void Awake()
        {
            SetCardUI();
        }

        public void SetCardUI()
        {
            if (card != null)
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
        
        #endregion

    #endregion
}
