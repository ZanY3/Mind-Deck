using UnityEngine;

public class AnxietyDebuff : MonoBehaviour
{
    [SerializeField] private int anxietyDamage = 2;
    [HideInInspector] public bool startAnxietyApplied = false;
    public int AnxietyDamage => anxietyDamage;
}
