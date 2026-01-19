using UnityEngine;

public class Boss : Enemy
{
    public override void AttackPlayer()
    {
        base.AttackPlayer();
        Debug.Log("The mind spawned enemies");
    }
}
