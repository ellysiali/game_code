using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phytobaer_TeleportState : TeleportState
{
    private Phytobaer phytobaer;

    public Phytobaer_TeleportState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, Phytobaer phytobaer) : base(entity, stateMachine, animationBoolName)
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
        if (isTeleportTimeOver)
        {
            stateMachine.ChangeState(phytobaer.idleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
