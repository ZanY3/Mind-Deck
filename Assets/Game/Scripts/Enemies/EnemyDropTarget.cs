using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyDropTarget : MonoBehaviour, IDropHandler
{
    private Enemy enemy;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
    }
    public void OnDrop(PointerEventData eventData) //OnCardDrop
    {
        CardData card = eventData.pointerDrag.GetComponent<CardDisplay>().cardToDisplay;

        if (card.type != CardData.CardType.Attack && card.type != CardData.CardType.SkillOnEnemy)
        {
            return;
        }
        if(card.type == CardData.CardType.Attack)
        {
            enemy.TakeDamage(card.power);
        }
        else if(card.type == CardData.CardType.SkillOnEnemy)
        {
            if(card.effect == CardData.Effect.Stun)
            {
                eventData.pointerDrag.GetComponent<CardEffects>().Stun(enemy);
            }
        }
        eventData.pointerDrag.GetComponent<CardDrag>().droppedOnTarget = true;
    }
}
