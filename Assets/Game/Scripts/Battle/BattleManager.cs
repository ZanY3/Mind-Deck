using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using TMPro;
using Unity.VisualScripting;

public class BattleManager : MonoBehaviour
{
    [SerializeField] private HandManager handManager;
    [SerializeField] private PlayerHealth player;

    [Header("UI")]
    [Space]
    [SerializeField] private GameObject endTurnBtn;
    [SerializeField] private GameObject finalPanel;
    [SerializeField] private TMP_Text finalTxt;

    [HideInInspector] public bool isPlayerTurn = true;

    private List<Enemy> enemies;

    private void Awake()
    {
        enemies = new List<Enemy>();
    }

    public void AddEnemy(Enemy enemy)
    {
        enemies.Add(enemy);
    }
    public void RemoveEnemy(Enemy enemy)
    {
        enemies.Remove(enemy);
    }
    public void StartBattle()
    {
        isPlayerTurn = true;
        endTurnBtn.SetActive(true);
        finalPanel.SetActive(false);
    }
    public void EndPlayerTurn() //When we press "End turn"
    {
        handManager.ClearHand();
        isPlayerTurn = false;
        endTurnBtn.SetActive(false);
        EnemyTurn();
    }
    public void EnemyTurn()
    {
        if(player.hasAnxiety)
        {
            Debug.LogWarning("Player took damage from anxiety debuff");
            player.TakeDamage(player.anxietyDamage);
        }
        StartCoroutine(EnemyAttack());
    }
    public void CheckPlayerWin()
    {
        if (enemies.Count <= 0)
        {
            //RoundEnded
            player.hasAnxiety = false;
            player.anxietyDamage = 0;

            player.UpdateUI();
            Debug.LogWarning("Anxiety = false");

            handManager.ClearHand();
            endTurnBtn.SetActive(false);
            finalPanel.SetActive(true);
            finalTxt.text = "Congratulations! You have cleared this round!";
        }
    }
    public void PlayerLose()
    {
        //RoundEnded
        player.hasAnxiety = false;
        player.anxietyDamage = 0;

        Debug.LogWarning("Anxiety = false");
        player.UpdateUI();

        for(int i = 0; i < enemies.Count; i++)
        {
            enemies[i].enabled = false;
        }
        handManager.ClearHand();
        endTurnBtn.SetActive(false);
        finalPanel.SetActive(true);
        finalTxt.text = "Oh no! You have lost this round!";
    }

    IEnumerator EnemyAttack()
    {
        if(player.hasAnxiety)
        {
            yield return new WaitForSeconds(1.5f);
        }
        for (int i = 0; i < enemies.Count; i++) // All enemies attack in turn
        {
            if (enemies[i].Data.enemyType == EnemyData.EnemyType.Defender && enemies[i].GetComponentInChildren<DefenseCell>() != null)
            {
                enemies[i].GetComponentInChildren<DefenseCell>().GainDefense();
                //Some animations for enemy attack
                yield return new WaitForSeconds(1.5f);
            }

            if (!player.hasAnxiety && enemies[i].Data.enemyType == EnemyData.EnemyType.Debuffer)
            {
                player.hasAnxiety = true;

                Debug.LogWarning("Anxiety = true");
                player.UpdateUI();

                player.anxietyDamage = enemies[i].GetComponent<AnxietyDebuff>().AnxietyDamage;
            }

            enemies[i].AttackPlayer();
            //Some animations for enemy attack
            yield return new WaitForSeconds(1.5f);

        }
        isPlayerTurn = true;
        endTurnBtn.SetActive(true);
        handManager.DrawHand();
    }
}
