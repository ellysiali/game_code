﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phytobaer_FollowState : FollowState
{
    private Phytobaer phytobaer;
    public Phytobaer_FollowState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, D_FollowState stateData, Phytobaer phytobaer) : base(entity, stateMachine, animationBoolName, stateData)
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
        if (isInMinPlayerRange)
        {
            stateMachine.ChangeState(phytobaer.idleState);
        }
        else if (!isInMinPlayerRange && outOfRange)
        {
            stateMachine.ChangeState(phytobaer.teleportState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
