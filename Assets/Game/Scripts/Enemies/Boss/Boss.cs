using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Boss : Enemy
{
    public Sprite phase2Sprite;
    private BossPhaseController phaseController;

    private int attackTurnCounter = 0;
    private Image image;

    protected override void Start()
    {
        base.Start();
        image = transform.GetChild(1).GetComponentInChildren<Image>();
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

        if(currentHealth < maxHealth / 2.5f)// PHASE 2
        {
            image.sprite = phase2Sprite;
            damage += 2;
            UpdateUI();
            attackTurnCounter = 0;
        }
    }
}
