using UnityEngine;

public class CardEffects : MonoBehaviour
{
//--------------Effects on player---------------------------
    public void RemoveAllDebuffs(PlayerHealth playerHealth)
    {
        playerHealth.ClearAllDebuffs();
        Debug.Log("All debufs had cleaned!");
    }

//--------------Effects on enemy---------------------------
    public void Stun(Enemy enemy)
    {
        enemy.ApplyStun(1);
        Debug.Log("Enemy stunned!");
    }
}
