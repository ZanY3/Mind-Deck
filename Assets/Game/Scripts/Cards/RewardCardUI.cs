using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class RewardCardUI : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private CardRewardManager rewardManager;
    private CardData data;
    private Vector3 startScale;
    private RectTransform rectTransform;

    private void Start()
    {
        data = GetComponent<CardDisplay>().cardToDisplay;
        rectTransform = GetComponent<RectTransform>();
        startScale = rectTransform.localScale;
    }
//--------------------------------------------------------------------------------------
    public void OnPointerClick(PointerEventData eventData)
    {
        rewardManager.hasChosenCard = true;
        rewardManager.ChooseCard(data);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOScale(startScale * 1.25f, 0.2f).SetEase(Ease.Linear);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(startScale, 0.2f).SetEase(Ease.Linear);
    }
}
