using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : State
{
    protected D_JumpState stateData;

    public JumpState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, D_JumpState stateData) : base(entity, stateMachine, animationBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        entity.SetVelocityX(stateData.movementSpeed);
    }

    public override void Enter()
    {
        base.Enter();
        entity.SetVelocityY(stateData.jumpVelocity);
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
