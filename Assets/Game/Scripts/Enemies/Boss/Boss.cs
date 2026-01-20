using UnityEngine;

public class Boss : Enemy
{
    private BossPhaseController phaseController;

    protected override void Start()
    {
        base.Start();
        phaseController = GetComponent<BossPhaseController>();
    }

    public override void AttackPlayer()
    {
        if(currentHealth > maxHealth / 2)
        {
            phaseController.SummonEnemies();
        }

        base.AttackPlayer();

        if(currentHealth < maxHealth / 2)//for test
        {
            damage += 2;
            UpdateUI();
        }
    }
}
