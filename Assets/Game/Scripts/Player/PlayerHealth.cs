using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private BattleManager battleManager;

    [SerializeField] private int maxHealth;

    private int currentHealth;
    private PlayerDefense defense;
    
    [HideInInspector] public bool hasAnxiety = false;
    [HideInInspector] public int anxietyDamage = 0;

    [HideInInspector] public bool stunned = false;

    [Space]
    [Header("UI/HealthBar")]

    [SerializeField] private GameObject stunClue;
    [SerializeField] private GameObject anxietyDebuffImg;
    [SerializeField] private GameObject stunDebuffImg;
    [SerializeField] private Image healthBarImage;
    [SerializeField] private TMP_Text healthTxt;

    [HideInInspector] public int turnsUntilStunRemove = 0;

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
    public void ChangeStunState(bool state)
    {
        stunned = state;
        stunDebuffImg.SetActive(state);
        UpdateUI();
    }
    public void ChangeStunClueState(bool state)
    {
        stunClue.SetActive(state);
    }
    public void ClearAllDebuffs()
    {
        anxietyDamage = 0;
        hasAnxiety = false;
        turnsUntilStunRemove = 0;
        ChangeStunState(false);
        UpdateUI();
    }
    public void UpdateUI()
    {
        if(healthTxt != null && healthBarImage != null)
        {
            anxietyDebuffImg.SetActive(hasAnxiety);
            stunDebuffImg.SetActive(stunned);

            if(hasAnxiety && !stunned)
            {
                GetComponent<Image>().color = new Color32(224, 255, 194, 255); //#E0FFC2
            }
            else if(stunned)
            {
                GetComponent<Image>().color = new Color32(206, 126, 255, 255); //#CE7EFF
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
