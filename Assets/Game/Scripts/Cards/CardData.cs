using UnityEngine;

[CreateAssetMenu(fileName = "New_Card", menuName = "Scriptable Objects/Card")]

public class CardData : ScriptableObject
{
    public enum CardType
    {
        Attack,
        Defence,
        SkillOnPlayer
    }
    public enum Effect
    {
        Empty,
        Cleansing
    }
    public CardType type;
    public Effect effect;
    public Sprite icon;
    public string description;
    public int power; //like attack damage or defence value
    public int energyCost;
}
