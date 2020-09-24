using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phytobaer_MoveState : MoveState
{
    private Phytobaer phytobaer;

    public Phytobaer_MoveState(Entity entity, FiniteStateMachine stateMachine, 
                           string animationBoolName, D_MoveState stateData, Phytobaer phytobaer) : 
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
        else if (isPlayerInMinAgroRange)
        {
            stateMachine.ChangeState(phytobaer.playerDetectedState);
        }
        else if (isMoveTimeOver)
        {
            stateMachine.ChangeState(phytobaer.idleState);
        }
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
