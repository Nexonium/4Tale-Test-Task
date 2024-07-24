using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

/// <summary>
/// Sets properties of the CardPrefab
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
    private Vector3 originalScale;
    private Vector3 originalPosition;
    public GameObject playField;

    public float scaleFactor = 1.2f;

    #endregion

    #region Methods and Classes
    private void Start()
    {
        originalScale = transform.localScale;
    }

    #region Selector and Drag methods
        public void OnPointerEnter(PointerEventData eventData)
        {
            originalScale = transform.localScale;
            transform.localScale = originalScale * scaleFactor;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            transform.localScale = originalScale;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            originalPosition = transform.position;
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = Input.mousePosition;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            RectTransform playFieldRect = playField.GetComponent<RectTransform>();
            if (RectTransformUtility.RectangleContainsScreenPoint(playFieldRect, Input.mousePosition))
            {
                // TODO: Play card effect
                //Destroy(gameObject);
            }
            transform.position = originalPosition;
        }
        #endregion

    #region Init object/prefab methods
    // Refresh object in inspector/editor window
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
