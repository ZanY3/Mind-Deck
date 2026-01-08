using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyData enemyData;
    [SerializeField] private TMP_Text healthTxt;
    [SerializeField] private TMP_Text damageTxt;

    [Space]
    [Header("Not required")]
    [SerializeField] private TMP_Text debuffDamageTxt;

    [HideInInspector] public bool stunned = false;

    private PlayerHealth player;

    private int currentHealth;
    private int damage;
    private BattleManager battleManager;
    [HideInInspector] public int stunTurnsLeft = 0;

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
        currentHealth -= value;
        UpdateUI();
        //Some effects
        if (currentHealth <= 0)
        {
            battleManager.RemoveEnemy(this);
            battleManager.CheckPlayerWin();
            Destroy(gameObject);
        }
    }
    public void ApplyStun(int turns)
    {
        GetComponent<EnemyToolTip>().UpdateStunToolTip(true);
        stunned = true;
    }
    public void AttackPlayer()
    {
        player.TakeDamage(enemyData.damage);
    }
}
