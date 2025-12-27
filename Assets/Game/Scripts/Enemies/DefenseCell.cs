using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class DefenseCell : MonoBehaviour, IDropHandler
{
    [SerializeField] private int defenseAmout;
    [SerializeField] private EnemyDropTarget dropTarget;

    [Space]
    [Header("UI")]
    [SerializeField] private TMP_Text defenseAmoutTxt;

    //[HideInInspector] public bool canAttackEnemy = false;

    private Enemy enemy;

    private void Start()
    {
        dropTarget.enabled = false;
        enemy = GetComponent<Enemy>();
        UpdateUI();
    }

    public void DecreaseDefense(int value)
    {
        if(defenseAmout > value)
        {
            defenseAmout -= value;
        }
        else if(defenseAmout <= value)
        {
            gameObject.SetActive(false);
            //canAttackEnemy = true;
            dropTarget.enabled = true;
        }
        UpdateUI();
    }
    public void UpdateUI()
    {
       defenseAmoutTxt.text = defenseAmout.ToString();
    }
    public void OnDrop(PointerEventData eventData) //OnCardDrop
    {
        CardData card = eventData.pointerDrag.GetComponent<CardDisplay>().cardToDisplay;

        if (card.type != CardData.CardType.Attack)
        {
            return;
        }
        else
        {
            DecreaseDefense(card.power);
        }
        eventData.pointerDrag.GetComponent<CardDrag>().droppedOnTarget = true;
    }
}
