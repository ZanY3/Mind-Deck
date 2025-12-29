using UnityEngine;
using UnityEngine.EventSystems;

public class CardDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private Vector2 startPosition;
    private Canvas canvas;
    private Canvas cardCanvas;
    private CardDisplay cardDisplay;
    private EnergyManager energyManager;

    [HideInInspector] public bool droppedOnTarget;

    private void Awake()
    {
        cardDisplay = GetComponent<CardDisplay>();
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        cardCanvas = GetComponent<Canvas>();
        energyManager = FindAnyObjectByType<EnergyManager>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        InteractionState.isDraggingCard = true;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        droppedOnTarget = false;
        startPosition = rectTransform.anchoredPosition;

        cardCanvas.overrideSorting = true;
        cardCanvas.sortingOrder = 100;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        InteractionState.isDraggingCard = false;
        cardCanvas.overrideSorting = false;
        GetComponent<CanvasGroup>().blocksRaycasts = true;

        CardData card = cardDisplay.cardToDisplay;

        GameObject target = eventData.pointerEnter;

        if (!droppedOnTarget || target == null ||
        (target.GetComponent<PlayerHealth>() != null && card.type == CardData.CardType.Attack) ||
        (target.GetComponent<Enemy>() != null && card.type == CardData.CardType.Defence) ||
        (energyManager.EnoughEnergyToPlayCard(cardDisplay.cardToDisplay.energyCost) == false))
        {
            rectTransform.anchoredPosition = startPosition;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
