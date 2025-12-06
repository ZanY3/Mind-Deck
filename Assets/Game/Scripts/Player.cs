using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    private int currentHealth;

    [Space]
    [Header("UI/HealthBar")]

    [SerializeField] private Image healthBarImage;
    [SerializeField] private TMP_Text healthTxt;

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateUI();
    }
    public void TakeDamage(int damage)
    {
        if(currentHealth > 0)
        {
            currentHealth -= damage;
            UpdateUI();
        }
        if(currentHealth <= 0)
        {
            Debug.Log("PLAYER DIED!!!");
            //Die window
        }
    }
    public void UpdateUI()
    {
        if(healthTxt != null && healthBarImage != null)
        {
            healthBarImage.fillAmount = (float)currentHealth / maxHealth;
            healthTxt.text = currentHealth.ToString() + "/" + maxHealth.ToString();
        }
    }
}
