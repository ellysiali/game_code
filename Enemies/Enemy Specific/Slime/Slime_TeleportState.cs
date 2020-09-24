using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_TeleportState : TeleportState
{
    private Slime slime;

    public Slime_TeleportState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, Slime slime) : base(entity, stateMachine, animationBoolName)
    {
        this.slime = slime;
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
            stateMachine.ChangeState(slime.idleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
