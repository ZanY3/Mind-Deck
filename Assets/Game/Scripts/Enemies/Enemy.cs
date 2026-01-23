using TMPro;
using UnityEngine;
using DG.Tweening;

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

    [HideInInspector] public int currentHealth;
    [HideInInspector] public int maxHealth;
    [HideInInspector] public int damage;

    private BattleManager battleManager;
    [HideInInspector] public int stunTurnsLeft = 0;

    public EnemyData Data => enemyData;

    protected virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        battleManager = FindAnyObjectByType<BattleManager>();

        battleManager.AddEnemy(this);

        ReadData();
        UpdateUI();
    }
//---------------------------------------------------------------------------------------------
    public void ReadData()
    {
        currentHealth = enemyData.health;
        maxHealth = currentHealth;
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
    public void TakeDamage(int value) //Maybe it will be overrided in boss to
    {
        currentHealth -= value;
        UpdateUI();
        //Some effects
        if (currentHealth <= 0)
        {
            if(enemyData.name == "Brain Leech")
            {
                FindAnyObjectByType<BossPhaseController>().enemiesSummonedCount--;
            }
            battleManager.RemoveEnemy(this);
            battleManager.CheckPlayerWin();
            Destroy(gameObject);
        }
    }
    public void ApplyStun()
    {
        GetComponentInChildren<EnemyToolTip>().UpdateStunToolTip(true);
        stunned = true;
    }
    public virtual void AttackPlayer()
    {
        Vector3 tempPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        transform.DOMoveX(player.GetComponent<Transform>().position.x, 0.2f).OnComplete(() =>
        {
            transform.DOMoveX(tempPos.x, 0.15f);
        });
        player.TakeDamage(damage);
    }
}
