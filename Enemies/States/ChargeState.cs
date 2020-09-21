﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeState : State
{
    protected D_ChargeState stateData;
    protected bool isPlayerInMinAgroRange, isDetectingLedge, isDetectingWall, isChargeTimeOver,
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
        isPlayerInMinAgroRange = entity.CheckEnemyInMinAgroRange();
        isDetectingLedge = entity.CheckLedge();
        isDetectingWall = entity.CheckWall();
        entity.SetVelocityX(stateData.chargeSpeed);

        performCloseRangeAction = entity.CheckEnemyInCloseRangeAction();
    }

    public override void Enter()
    {
        base.Enter();
        isChargeTimeOver = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Time.time >= startTime + stateData.chargeTime)
        {
            isChargeTimeOver = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}