using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_PlayerDetectedState : PlayerDetectedState
{
    private Slime slime;

    public Slime_PlayerDetectedState(Entity entity, FiniteStateMachine stateMachine, 
                                     string animationBoolName, D_PlayerDetectedState stateData, Slime slime) : 
                                     base(entity, stateMachine, animationBoolName, stateData)
    {
        this.slime = slime;
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
        if (entity.isFriendly && !slime.CheckPlayerInMaxRange())
        {
            stateMachine.ChangeState(slime.followState);
        }
        else if (performCloseRangeAction)
        {
            stateMachine.ChangeState(slime.meleeAttackState);
        }
        else if (performLongRangeAction)
        {
            if (!isDetectingLedge || isDetectingWall)
            {
                stateMachine.ChangeState(slime.jumpState);
            }
            else
            { 
                stateMachine.ChangeState(slime.chargeState); 
            }
        }
        else if (!isPlayerInMaxAgroRange)
        {
            stateMachine.ChangeState(slime.lookForPlayerState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
