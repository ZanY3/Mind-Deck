using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    [HideInInspector] public CardData cardToDisplay;

    [SerializeField] private Image iconImg;
    [SerializeField] private TMP_Text nameTxt;
    [SerializeField] private TMP_Text descriptionTxt;
    [SerializeField] private TMP_Text typeTxt;

    [Space]
    [Header("Color settings")]
    [SerializeField] private Image body;
    [SerializeField] private Color attackCardColor;
    [SerializeField] private Color defenceCardColor;


    public void VisualizeCard()
    {
        if (cardToDisplay == null)
        {
            Debug.LogError("VisualizeCard called with NULL CardData");
            return;
        }

        if (cardToDisplay.type == CardData.CardType.Attack)
        {
            body.color = attackCardColor;
        }
        else if(cardToDisplay.type == CardData.CardType.Defence)
        {
            body.color = defenceCardColor;
        }

        nameTxt.text = cardToDisplay.name;
        descriptionTxt.text = cardToDisplay.description;
        typeTxt.text = cardToDisplay.type.ToString();

        iconImg.sprite = cardToDisplay.icon;
    }
}
