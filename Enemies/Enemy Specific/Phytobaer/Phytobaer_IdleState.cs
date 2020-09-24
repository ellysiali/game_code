using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phytobaer_IdleState : IdleState
{
    private Phytobaer phytobaer;
    public Phytobaer_IdleState(Entity entity, FiniteStateMachine stateMachine, 
                           string animationBoolName, D_IdleState stateData, Phytobaer phytobaer) : 
                           base(entity, stateMachine, animationBoolName, stateData)
    {
        this.phytobaer = phytobaer;
    }
    public override void Enter()
    {
        base.Enter();
    }
    public override void Exit()
    {
        base.Exit();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (entity.isFriendly && !phytobaer.CheckPlayerInMaxRange())
        {
            stateMachine.ChangeState(phytobaer.followState);
        }
        else if (isEnemyInMinAggroRange)
        {
            stateMachine.ChangeState(phytobaer.chargeState);
        }
        else if (isIdleTimeOver)
        {
            stateMachine.ChangeState(phytobaer.moveState);
        }
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
