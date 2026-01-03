using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

public class CardRewardManager : MonoBehaviour
{
    [SerializeField] private CardData[] allCards;
    [SerializeField] private CardDisplay[] cardTemplates;
    [SerializeField] private DeckManager deckManager;

    [HideInInspector] public bool hasChosenCard = false;
    
    private int cardsCount;

    public void GetRewardCards(int count)
    {
        cardsCount = count;
        for (int i = 0; i < count; i++)
        {
            int randNum = Random.Range(0, allCards.Length - 1);
            cardTemplates[i].cardToDisplay = allCards[randNum];
            cardTemplates[i].VisualizeCard();
        }
    }
    public void SetCardsInteractable(bool state)
    {
        for (int i = 0; i < cardsCount; i++)
        {
            CanvasGroup cg = cardTemplates[i].GetComponent<CanvasGroup>();
            cg.interactable = state;
            cg.blocksRaycasts = state;
        }
    }

    public void ChooseCard(CardData card)
    {
        SetCardsInteractable(false);
        deckManager.AddCard(card);
    }
}
