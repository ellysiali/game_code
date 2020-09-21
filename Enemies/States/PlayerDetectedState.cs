using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetectedState : State
{
    protected D_PlayerDetectedState stateData;

    protected bool isPlayerInMinAgroRange, isPlayerInMaxAgroRange, performLongRangeAction,
                   performCloseRangeAction, isDetectingLedge, isDetectingWall;

    public PlayerDetectedState(Entity entity, FiniteStateMachine stateMachine,
                                     string animationBoolName, D_PlayerDetectedState stateData) :
                                     base(entity, stateMachine, animationBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        isDetectingLedge = entity.CheckLedge();
        isDetectingWall = entity.CheckWall();
        isPlayerInMinAgroRange = entity.CheckEnemyInMinAgroRange();
        isPlayerInMaxAgroRange = entity.CheckEnemyInMaxAgroRange();

        performCloseRangeAction = entity.CheckEnemyInCloseRangeAction();
    }

    public override void Enter()
    {
        base.Enter();
        performLongRangeAction = false;
        if (!entity.CheckFacingEnemy())
        {
            entity.Flip();
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Time.time >= startTime + stateData.longRangeActionTime)
        {
            performLongRangeAction = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
