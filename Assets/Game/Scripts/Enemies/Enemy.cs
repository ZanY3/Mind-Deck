using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyData enemyData;
    [SerializeField] private TMP_Text healthTxt;
    [SerializeField] private TMP_Text damageTxt;

    [Space]
    [Header("Not required")]
    [SerializeField] private TMP_Text debuffDamageTxt;

    private PlayerHealth player;

    private int currentHealth;
    private int damage;
    private BattleManager battleManager;

    public EnemyData Data => enemyData;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        battleManager = FindAnyObjectByType<BattleManager>();

        battleManager.AddEnemy(this);

        ReadData();
        UpdateUI();
    }
    public void ReadData()
    {
        currentHealth = enemyData.health;
        damage = enemyData.damage;
    }
    public void UpdateUI()
    {
        healthTxt.text = currentHealth.ToString();
        damageTxt.text = damage.ToString();
        if(debuffDamageTxt != null)
        {
            debuffDamageTxt.text = GetComponent<AnxietyDebuff>().AnxietyDamage.ToString();
        }
    }
    public void TakeDamage(int value)
    {
        if (currentHealth >= value)
        {
            currentHealth -= value;
            UpdateUI();
            //Some effects
        }
        if(currentHealth <= 0)
        {
            battleManager.RemoveEnemy(this);
            battleManager.CheckPlayerWin();
            Destroy(gameObject);
        }
    }
    public void AttackPlayer()
    {
        player.TakeDamage(enemyData.damage);
    }
}
