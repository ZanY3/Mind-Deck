using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerDropTarget : MonoBehaviour, IDropHandler
{
    private PlayerDefense defense;
    private void Start()
    {
        defense = GetComponent<PlayerDefense>();
    }
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
            if(eventData.pointerDrag.GetComponent<CardDisplay>().cardToDisplay.effect == CardData.Effect.Cleansing)
            {
                eventData.pointerDrag.GetComponent<CardEffects>().RemoveAllDebuffs(GetComponent<PlayerHealth>());
            }
        }
         eventData.pointerDrag.GetComponent<CardDrag>().droppedOnTarget = true;
    }
}
