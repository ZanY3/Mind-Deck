using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;

public class BattleManager : MonoBehaviour
{
    [SerializeField] private GameObject EndTurnBtn;
    [SerializeField] private HandManager handManager;

    [HideInInspector] public bool isPlayerTurn = true;

    public List<Enemy> enemies;

    public void AddEnemy(Enemy enemy)
    {
        enemies.Add(enemy);
    }
    public void RemoveEnemy(Enemy enemy)
    {
        enemies.Remove(enemy);
    }

    public void EndPlayerTurn() //When we press "End turn"
    {
        handManager.ClearHand();
        isPlayerTurn = false;
        EndTurnBtn.SetActive(false);
        EnemyTurn();
    }
    public void EnemyTurn()
    {
        StartCoroutine(EnemyAttack());
    }
    IEnumerator EnemyAttack()
    {
        for (int i = 0; i < enemies.Count; i++)// All enemies attack in turn
        {
            enemies[i].AttackPlayer();
            //Some animations for enemy attack
            yield return new WaitForSeconds(1.5f);
        }
        isPlayerTurn = true;
        EndTurnBtn.SetActive(true);
        handManager.DrawHand();
    }
}
