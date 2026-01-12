using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class BasicTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject tooltip;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!InteractionState.isDraggingCard)
        {
            ShowUI();
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (!InteractionState.isDraggingCard)
        {
            HideUI();
        }
    }
    public void ShowUI()
    {
        tooltip.SetActive(true);
    }
    public void HideUI()
    {
        tooltip.SetActive(false);
    }
}
