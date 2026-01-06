using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private BattleManager battleManager;

    private int currentHealth;
    private PlayerDefense defense;
    
    [HideInInspector] public bool hasAnxiety = false;
    [HideInInspector] public int anxietyDamage = 0;

    [Space]
    [Header("UI/HealthBar")]

    [SerializeField] private GameObject debuffImage;
    [SerializeField] private Image healthBarImage;
    [SerializeField] private TMP_Text healthTxt;

    private void Start()
    {
        defense = GetComponent<PlayerDefense>();
        currentHealth = maxHealth;
        UpdateUI();
    }
    public void TakeDamage(int damage)
    {
        if(currentHealth > 0)
        {
            currentHealth -= defense.CalculateDamage(damage);
            UpdateUI();
        }
        if(currentHealth <= 0)
        {
            currentHealth = 0;
            UpdateUI();
            battleManager.PlayerLose();//Fix bugs with UI and with enemy disable
        }
    }
    public void ClearAllDebuffs()
    {
        anxietyDamage = 0;
        hasAnxiety = false;
        UpdateUI();
    }
    public void UpdateUI()
    {
        if(healthTxt != null && healthBarImage != null)
        {
            debuffImage.SetActive(hasAnxiety);
            if(hasAnxiety)
            {
                GetComponent<Image>().color = new Color32(224, 255, 194, 255); //#E0FFC2
            }
            else
            {
                GetComponent<Image>().color = Color.white;
            }
            healthBarImage.fillAmount = (float)currentHealth / maxHealth;
            healthTxt.text = currentHealth.ToString() + "/" + maxHealth.ToString();
        }
    }
}
