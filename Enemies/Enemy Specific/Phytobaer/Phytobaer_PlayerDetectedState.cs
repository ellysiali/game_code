using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phytobaer_PlayerDetectedState : PlayerDetectedState
{
    private Phytobaer phytobaer;

    public Phytobaer_PlayerDetectedState(Entity entity, FiniteStateMachine stateMachine, 
                                     string animationBoolName, D_PlayerDetectedState stateData, Phytobaer phytobaer) : 
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
        else if (performCloseRangeAction)
        {
            stateMachine.ChangeState(phytobaer.meleeAttackState);
        }
        else if (performLongRangeAction)
        {
            if (!isDetectingLedge || isDetectingWall)
            {
                stateMachine.ChangeState(phytobaer.jumpState);
            }
            else
            { 
                stateMachine.ChangeState(phytobaer.chargeState); 
            }
        }
        else if (!isPlayerInMaxAgroRange)
        {
            stateMachine.ChangeState(phytobaer.lookForPlayerState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
