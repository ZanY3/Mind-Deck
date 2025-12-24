using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    [SerializeField] private int currentStage = 1;
    [SerializeField] private int numberOfStages = 5;

    [Space]
    [Header("Enemies indexes equal to stage enemies to spawn")]
    [SerializeField] private List<GameObject> enemiesPrefabs;

    [Space]
    [Header("Managers/Objects")]
    [SerializeField] HandManager handManager;
    [SerializeField] RectTransform enemySlotPos;
    [SerializeField] BattleManager battleManager;

    private void Start()
    {
        StartStage();
    }

    public void WinBattle()
    {
        if(currentStage != numberOfStages)
        {
            currentStage++;
            battleManager.StartBattle();
            StartStage();
        }
        else
        {
            Debug.LogWarning("All stages was completed");
            SceneManager.LoadScene("Game");
        }
    }
    public void StartStage()
    {
        var enemy = Instantiate(enemiesPrefabs[currentStage - 1], enemySlotPos.position, Quaternion.identity);
        enemy.transform.SetParent(enemySlotPos.transform, false);
        handManager.DrawHand();
    }

}
