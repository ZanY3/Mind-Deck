using UnityEngine;

public class CardEffects : MonoBehaviour
{
    public void RemoveAllDebuffs(PlayerHealth playerHealth)
    {
        playerHealth.ClearAllDebuffs();
        Debug.Log("All debufs had cleaned!");
    }
}
