using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeState : State
{
    protected D_ChargeState stateData;
    protected bool isPlayerInMinAggroRange, isPlayerInMaxAggroRange, isDetectingLedge, isDetectingWall, isChargeTimeOver,
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
        if (entity.CheckEnemyInMaxAggroRange() && !entity.CheckFacingEnemy() && entity.CheckCanAttack())
        {
            entity.Flip();
        }
        entity.SetVelocityX(stateData.movementSpeed);

        if ((!entity.CheckLedge() || entity.CheckWall()) && entity.CheckGround())
        {
            entity.SetVelocityY(stateData.jumpVelocity);
        }

        isPlayerInMinAggroRange = entity.CheckEnemyInMinAggroRange();
        isPlayerInMaxAggroRange = entity.CheckEnemyInMaxAggroRange();
        performCloseRangeAction = entity.CheckEnemyInCloseRangeAction();
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
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
