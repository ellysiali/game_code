﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terracto_TeleportState : TeleportState
{
    private Terracto terracto;

    public Terracto_TeleportState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, Terracto terracto) : base(entity, stateMachine, animationBoolName)
    {
        this.terracto = terracto;
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
        if (isTeleportTimeOver)
        {
            stateMachine.ChangeState(terracto.idleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
