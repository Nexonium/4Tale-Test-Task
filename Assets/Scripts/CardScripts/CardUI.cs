using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

/// <summary>
/// Sets and manages properties of the CardPrefab, card drag, selection, and other properties
/// </summary>

public class CardUI : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
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

    //public BezierArrow bezierArrow;
    //public GameObject arrow;
    public ArrowArcRenderer arrow;
    private GameObject playField;

    private RectTransform rectTransform;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private int originalSiblingIndex;

    public float scaleFactor = 1.2f;
    //public Vector3 hoverOffset = new(0, 20, 0);


    [Header("Selector Properties")]
    private Deck deck;
    private HandFieldController handFieldController;

    private bool isHighlighted = false;

    #endregion

    #region Methods and Classes
    private void Start()
    {
        deck = FindObjectOfType<Deck>();
        handFieldController = FindObjectOfType<HandFieldController>();
        rectTransform = GetComponent<RectTransform>();
        arrow = GetComponent<ArrowArcRenderer>();
    }

    void Update()
    {

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
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (card.HasTargetableEffect())
            {
                if (!isTargeting) { 
                    StartTargeting(eventData.position); 
                }
                else
                {
                    GameObject target = eventData.pointerCurrentRaycast.gameObject;
                    if (target != null)
                    {
                        // TODO: Play card
                        //ApplyEffects(target.GetComponent<Enemy>);
                    }
                }
            }
            else
            {
                // TODO: Play card
                //ApplyEffects();
                //Destroy(gameObject);
            }
        }
        else if ((eventData.button == PointerEventData.InputButton.Right) && isTargeting) 
        {
            if (isHighlighted)
            {
                CancelHighlight();
            }
            else
            {
                CancelSelection();
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (card.HasTargetableEffect())
        {
            isTargeting = true;
        }
        else
        {
            isDragging = true;
            originalPosition = transform.position;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDragging)
        {
            transform.position = eventData.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {

        //if (isTargeting)
        //{
        //    GameObject target = eventData.pointerCurrentRaycast.gameObject;
        //    if (target != null)
        //    {
        //        //ApplyEffects(target.GetComponent<Enemy>)
        //    }
        //    else
        //    {
        //        CancelSelection();
        //    }
        //}
        if (isDragging)
        {

            playField = FindAnyObjectByType<PlayField>().gameObject;
            if (playField != null)
            {

                RectTransform playFieldRect = playField.GetComponent<RectTransform>();
                if (RectTransformUtility.RectangleContainsScreenPoint(playFieldRect, Input.mousePosition))
                {
                    deck.DiscardCard(card);
                    
                    // Update hand
                    handFieldController.UpdateHandDisplay(deck.handPile.ToArray());

                    // TODO: Play card effect
                    Destroy(gameObject);
                }
            }

            transform.position = originalPosition;
        }
    }

    private void HighlightCard()
    {
        isHighlighted = true;

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

    private void CancelHighlight()
    {
        isHighlighted = false;

        // Returning original properties
        //rectTransform.position = originalPosition;
        rectTransform.localRotation = originalRotation;
        rectTransform.localScale = Vector3.one;
        rectTransform.SetSiblingIndex(originalSiblingIndex);
    }

    private void StartTargeting(Vector3 position)
    {
        isTargeting = true;
        arrow.enabled = true;
    }

    private void CancelSelection()
    {
        isTargeting = false;
        arrow.enabled = false;
        //CancelHighlight();
        //ResetCard();
    }

    private void ResetCard()
    {
        isDragging = false;
        isTargeting = false;
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
