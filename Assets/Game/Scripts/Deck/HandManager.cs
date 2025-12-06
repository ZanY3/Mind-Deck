using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    [SerializeField] private GameObject cardPrefab; //Without pool system yet
    [SerializeField] private List<CardData> cards;
    [SerializeField] private Transform handParent;

    private int handSize = 3;

    private void Start()
    {
        DrawHand();
    }

    public void DrawHand()
    {
        ClearHand();
        for (int i = 0; i < handSize; i++)
        {
            CardData data = cards[Random.Range(0, cards.Count)];
            SpawnCard(data);
        }

    }
    public void SpawnCard(CardData data)
    {
        GameObject card = Instantiate(cardPrefab, handParent);
        card.GetComponent<CardDisplay>().cardToDisplay = data;
    }
    public void ClearHand()
    {
        foreach (Transform child in handParent)
            Destroy(child.gameObject);
    }
}
