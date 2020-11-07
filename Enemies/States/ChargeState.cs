using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeState : State
{
    protected D_ChargeState stateData;
    protected bool isEnemyInMinAggroRange, isEnemyInMaxAggroRange, isDetectingLedge, isDetectingWall, isChargeTimeOver,
                   performCloseRangeAction;

    public ChargeState(Entity entity, FiniteStateMachine stateMachine,
                                     string animationBoolName, D_ChargeState stateData) :
                                     base(entity, stateMachine, animationBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        entity.SetVelocityX(stateData.movementSpeed);

        if ((!entity.CheckLedge() || entity.CheckWall()) && entity.CheckGround())
        {
            if (entity.CheckFacingEnemy())
            {
                entity.SetVelocityY(stateData.jumpVelocity);
            }
            else
            {
                entity.Flip();
            }
        }

        isEnemyInMinAggroRange = entity.CheckEnemyInMinAggroRange();
        isEnemyInMaxAggroRange = entity.CheckEnemyInMaxAggroRange();
        performCloseRangeAction = entity.CheckEnemyInCloseRangeAction();
    }

    public override void Enter()
    {
        base.Enter();
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
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
