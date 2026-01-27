using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using DG.Tweening;

public class BattleManager : MonoBehaviour
{
    [SerializeField] private HandManager handManager;
    [SerializeField] private EnergyManager energyManager;
    [SerializeField] private PlayerHealth player;

    [Header("UI")]
    [Space]
    [SerializeField] private GameObject endTurnBtn;
    [SerializeField] private GameObject handPanel;

    [SerializeField] private GameObject winFinalPanel;
    [SerializeField] private GameObject loseFinalPanel;

    [HideInInspector] public bool isPlayerTurn = true;

    [HideInInspector] public List<Enemy> enemies;

    private void Awake()
    {
        enemies = new List<Enemy>();
    }
//-------------------------------------------
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

        EndBtnSetActive(true);

        winFinalPanel.SetActive(false);
        loseFinalPanel.SetActive(false);

        energyManager.RefillEnergy();
    }
    public void EndPlayerTurn() //When we press "End turn"
    {
        energyManager.RefillEnergy();
        handManager.ClearHand();
        isPlayerTurn = false;

        EndBtnSetActive(false);
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

            handManager.ClearHand();
            EndBtnSetActive(false);
            winFinalPanel.SetActive(true);
        }
    }
    public void PlayerLose()
    {
        //RoundEnded
        player.hasAnxiety = false;
        player.anxietyDamage = 0;

        player.UpdateUI();

        for(int i = 0; i < enemies.Count; i++)
        {
            enemies[i].enabled = false;
        }
        EndBtnSetActive(false);
        loseFinalPanel.SetActive(true);
        handPanel.SetActive(false);
    }

    IEnumerator EnemyAttack()
    {
        if(player.hasAnxiety)
        {
            yield return new WaitForSeconds(1.5f);
        }
        for (int i = 0; i < enemies.Count; i++) // All enemies attack in turn
        {
            if (enemies[i].Data.enemyType == EnemyData.EnemyType.Defender && enemies[i].GetComponentInChildren<DefenseCell>() != null && enemies[i].stunned == false)
            {
                enemies[i].GetComponentInChildren<DefenseCell>().RefillDefense();
                //Some animations for enemy attack
                yield return new WaitForSeconds(1.5f);
            }

            if(enemies[i].Data.enemyType == EnemyData.EnemyType.Debuffer)
            {
                if (enemies[i].GetComponent<AnxietyDebuff>() != null)
                {
                    if (!player.hasAnxiety && !enemies[i].GetComponent<AnxietyDebuff>().startAnxietyApplied && enemies[i].stunned == false)
                    {
                        enemies[i].GetComponent<AnxietyDebuff>().startAnxietyApplied = true;
                        player.hasAnxiety = true;

                        Debug.LogWarning("Anxiety = true");
                        player.UpdateUI();

                        player.anxietyDamage = enemies[i].GetComponent<AnxietyDebuff>().AnxietyDamage;
                    }
                }
                else if (enemies[i].GetComponent<StunDebuff>() != null && enemies[i].stunned == false)
                {
                    StunDebuff enemyStun = enemies[i].GetComponent<StunDebuff>();
                    EnemyToolTip enemyToolTip = enemies[i].GetComponentInChildren<EnemyToolTip>();

                    if(enemyStun.turnsUntilStun > 0)
                    {
                        enemyStun.turnsUntilStun--;
                    }
                    if (enemyStun.turnsUntilStun <= 0)
                    {
                        enemyStun.DealStun();

                        enemyToolTip.UpdateStunClue(false);
                    }
                    if (enemyStun.turnsUntilStun == 1)
                    {
                        enemyToolTip.UpdateStunClue(true);
                    }
                 }
            }

            if (enemies[i].stunned)
            {
                enemies[i].stunTurnsLeft--;
                if (enemies[i].stunTurnsLeft <= 0)
                {
                    enemies[i].GetComponentInChildren<EnemyToolTip>().UpdateStunToolTip(false);
                    enemies[i].stunned = false;
                }
                continue;
            }
            enemies[i].AttackPlayer();
            //Some animations for enemy attack
            yield return new WaitForSeconds(1.5f);
        }
        if (player.stunned == false)
        {
            player.ChangeStunClueState(false);
            isPlayerTurn = true;
            EndBtnSetActive(true);
            handManager.DrawHand();
        }
        else if (player.stunned == true)
        {
            EndPlayerTurn();
            player.ChangeStunClueState(true);
            if (player.turnsUntilStunRemove <= 0)
            {
                player.ChangeStunState(false);
            }
            else
            {
                player.turnsUntilStunRemove--;
            }
        }
    }
    public void EndBtnSetActive(bool state)
    {
        RectTransform endBtnTransform = endTurnBtn.GetComponent<RectTransform>();

        if(!state)
        {
            endBtnTransform.DOAnchorPosY(-200, 0.3f).OnComplete(() =>
            {
                endTurnBtn.SetActive(false);
            });
        }
        else
        {
            endTurnBtn.SetActive(true);
            endBtnTransform.DOAnchorPosY(135, 0.3f);
        }
    }
}
