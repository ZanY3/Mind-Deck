using UnityEngine;

public class Boss : Enemy
{
    private BossPhaseController phaseController;

    private int attackTurnCounter = 0;

    protected override void Start()
    {
        base.Start();
        phaseController = GetComponent<BossPhaseController>();
    }

    public override void AttackPlayer()
    {
        attackTurnCounter++;

        if(attackTurnCounter == 2 || attackTurnCounter == 6 || attackTurnCounter == 10)
        {
            phaseController.SummonEnemies();
        }

        base.AttackPlayer();

        if(currentHealth < maxHealth / 2.5f)//for test
        {
            attackTurnCounter = 0;
            damage += 2;
            UpdateUI();
        }
    }
}
