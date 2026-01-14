using UnityEngine;
using UnityEngine.EventSystems;

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
        //A little bit later i will replace this on doTween animation
        rectTransform.localScale = startScale * 1.25f;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        rectTransform.localScale = startScale;
    }
}
