using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportState : State
{
    private float teleportTime = 0.1f;
    protected bool isTeleportTimeOver;
    public TeleportState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName) : base(entity, stateMachine, animationBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        entity.transform.position = entity.CheckPlayerPosition();
        isTeleportTimeOver = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Time.time >= startTime + teleportTime)
        {
            isTeleportTimeOver = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
