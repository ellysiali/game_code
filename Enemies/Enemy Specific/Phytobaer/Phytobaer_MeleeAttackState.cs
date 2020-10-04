using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phytobaer_MeleeAttackState : MeleeAttackState
{
    private Phytobaer phytobaer;
    public Phytobaer_MeleeAttackState(Entity entity, FiniteStateMachine stateMachine, 
                                  string animationBoolName, Transform attackPosition, 
                                  D_MeleeAttackState stateData, Phytobaer phytobaer) : 
                                  base(entity, stateMachine, animationBoolName, attackPosition, stateData)
    {
        this.phytobaer = phytobaer;
    }
    public override void DoChecks()
    {
        base.DoChecks();
    }
    public override void Enter()
    {
        base.Enter();
    }
    public override void Exit()
    {
        base.Exit();
    }
    public override void FinishAttack()
    {
        base.FinishAttack();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isAnimationFinished)
        {
            stateMachine.ChangeState(phytobaer.chargeState);
        }
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    public override void TriggerAttack()
    {
        base.TriggerAttack();
    }
}
