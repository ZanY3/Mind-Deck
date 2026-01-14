using UnityEngine;

public class StunDebuff : MonoBehaviour
{
    public int turnsUntilStun = 2;
    private int startTurnsUntilStun;

    private void Start()
    {
        startTurnsUntilStun = turnsUntilStun;
    }
    public void DealStun()
    {
        FindAnyObjectByType<PlayerHealth>().ChangeStunState(true);
        turnsUntilStun = startTurnsUntilStun;
    }
}
