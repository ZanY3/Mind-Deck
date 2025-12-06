using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField] private GameObject EndTurnBtn;
    [SerializeField] private HandManager handManager;

    [HideInInspector] public bool isPlayerTurn = true;

    public void EndPlayerTurn() //When we press "End turn"
    {
        handManager.ClearHand();
        isPlayerTurn = false;
        EndTurnBtn.SetActive(false);
        EnemyTurn();
    }
    public void EnemyTurn()
    {
        //Enemy attackss
        isPlayerTurn = true;
        EndTurnBtn.SetActive(true);
        handManager.DrawHand();

    }
}
