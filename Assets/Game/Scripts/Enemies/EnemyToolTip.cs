using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EnemyToolTip : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private EnemyData enemyData;

    [Space]
    [Header("UI/Tooltip")]
    [SerializeField] private GameObject tooltip;
    [SerializeField] private GameObject stunTooltip;
    [SerializeField] private Image iconImg;
    [SerializeField] private TMP_Text nameTxt;
    [SerializeField] private TMP_Text descriptionTxt;
    [SerializeField] private TMP_Text typeTxt;


    private void Start()
    {
        FillUI();
    }

    public void FillUI()
    {
        nameTxt.text = enemyData.name;
        descriptionTxt.text = enemyData.description;
        typeTxt.text = enemyData.enemyType.ToString();
        iconImg.sprite = enemyData.artwork;
    }
    public void UpdateStunToolTip(bool state)
    {
        stunTooltip.SetActive(state);
        if(state)
        {
            iconImg.color = new Color32(216, 196, 255, 255); // #D8C4FF
        }
        else
        {
            iconImg.color = Color.white;
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(!InteractionState.isDraggingCard)
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
