using UnityEngine;

public class BossPhaseController : MonoBehaviour
{
    //[SerializeField] private float damageMultiplier;

    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private RectTransform[] enemiesSpawnPos;

    [HideInInspector] public int enemiesSummonedCount = 0;

    private RectTransform enemySlot;

    private void Start()
    {
        enemySlot = GameObject.FindGameObjectWithTag("EnemySlot").GetComponent<RectTransform>();
    }

    public void SummonEnemies()
    {
        if(enemiesSummonedCount == 0)
        {
            for (int i = 0; i < enemiesSpawnPos.Length; i++)
            {
                int randNum = Random.Range(0, enemiesSpawnPos.Length);
                var enemy = Instantiate(enemyPrefabs[randNum]);

                enemy.transform.SetParent(enemySlot.transform, false);
                enemy.GetComponent<RectTransform>().position = enemiesSpawnPos[i].position;

                enemiesSummonedCount++;
                enemy.GetComponent<Enemy>().ApplyStun();
            }
            Debug.Log("Boss spawned enemies");
        }
        else
        {
            Debug.Log("Enemies was already spawned");
        }

    }
}
