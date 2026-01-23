using DG.Tweening;
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
        base.AttackPlayer();

        if((attackTurnCounter == 2 || attackTurnCounter == 6 || attackTurnCounter == 10) && stunned == false)
        {
            DOVirtual.DelayedCall(0.5f, () =>
            {
                phaseController.SummonEnemies();
            });
        }

        if(currentHealth < maxHealth / 2.5f)//for tests
        {
            damage += 2;
            UpdateUI();
            attackTurnCounter = 0;
        }
    }
}
