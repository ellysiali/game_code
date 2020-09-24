using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phytobaer_ChargeState : ChargeState
{
    private Phytobaer phytobaer;
    public Phytobaer_ChargeState(Entity entity, FiniteStateMachine stateMachine, 
                             string animationBoolName, D_ChargeState stateData, Phytobaer phytobaer) : 
                             base(entity, stateMachine, animationBoolName, stateData)
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

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (performCloseRangeAction)
        {
            stateMachine.ChangeState(phytobaer.meleeAttackState);
        }

        else if (isDetectingWall || !isDetectingLedge)
        {
            stateMachine.ChangeState(phytobaer.lookForPlayerState);
        }

        else if (isChargeTimeOver)
        {
            if (isPlayerInMinAgroRange)
            {
                stateMachine.ChangeState(phytobaer.playerDetectedState);
            }
            else
            {
                stateMachine.ChangeState(phytobaer.lookForPlayerState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
