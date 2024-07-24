using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

/// <summary>
/// Sets properties of the CardPrefab, drag and selection
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


    [Header("Selecter Properties")]
    public GameObject playField;

    private RectTransform rectTransform;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private int originalSiblingIndex;

    public float scaleFactor = 1.2f;
    public Vector3 hoverOffset = new(0, 20, 0);

    #endregion

    #region Methods and Classes
    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void SetCard(Card card)
    {
        cardName.text = card.cardName;
        cardDescription.text = card.cardDescription;
        cardCost.text = card.cardCost.ToString();
        cardArt.sprite = card.cardArt;
    }

    #region Selector and Drag methods

        public void OnPointerEnter(PointerEventData eventData)
        {
            // Saving original properties
            originalPosition = rectTransform.anchoredPosition;
            originalRotation = rectTransform.localRotation;
            originalSiblingIndex = rectTransform.GetSiblingIndex();

            // Setting new properties
            rectTransform.SetSiblingIndex(rectTransform.parent.childCount - 1);
            rectTransform.localScale = Vector3.one * scaleFactor;
            rectTransform.localRotation = Quaternion.Euler(0, 0, 0);
            //rectTransform.anchoredPosition = hoverOffset;

        }

        public void OnPointerExit(PointerEventData eventData)
        {
            // Returning original properties
            rectTransform.localScale = Vector3.one;
            rectTransform.anchoredPosition = originalPosition;
            rectTransform.localRotation = originalRotation;
            rectTransform.SetSiblingIndex(originalSiblingIndex);

        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            //originalPosition = transform.position;
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = Input.mousePosition;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            //RectTransform playFieldRect = playField.GetComponent<RectTransform>();
            //if (RectTransformUtility.RectangleContainsScreenPoint(playFieldRect, Input.mousePosition))
            //{
            //    // TODO: Play card effect
            //    //Destroy(gameObject);
            //}
            transform.position = originalPosition;
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
