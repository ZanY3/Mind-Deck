using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerDropTarget : MonoBehaviour, IDropHandler
{
    private PlayerDefense defense;

    private void Start()
    {
        defense = GetComponent<PlayerDefense>();
    }
//--------------------------------------------------------------------------------------
    public void OnDrop(PointerEventData eventData)
    {
        CardData card = eventData.pointerDrag.GetComponent<CardDisplay>().cardToDisplay;

        if (card.type != CardData.CardType.Defence && card.type != CardData.CardType.SkillOnPlayer)
        {
            return;
        }
        else if (card.type == CardData.CardType.Defence)
        {
            defense.AddArmor(card.power);
        }
        else if (card.type == CardData.CardType.SkillOnPlayer)
        {
            CardData cardTemp = eventData.pointerDrag.GetComponent<CardDisplay>().cardToDisplay;
            if (cardTemp.effect == CardData.Effect.Cleansing && GetComponent<PlayerHealth>().hasAnxiety)
            {
                eventData.pointerDrag.GetComponent<CardEffects>().RemoveAllDebuffs(GetComponent<PlayerHealth>());
            }
            if(cardTemp.effect == CardData.Effect.BloodPact)
            {
                FindAnyObjectByType<EnergyManager>().IncreaseEnergy(1);
                GetComponent<PlayerHealth>().TakeDamage(4);
            }
            else
            {
                Debug.Log("Player don't have debuffs to clean");
                return;
            }
        }
        eventData.pointerDrag.GetComponent<CardDrag>().droppedOnTarget = true;
    }
}
