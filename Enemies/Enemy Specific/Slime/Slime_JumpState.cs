using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_JumpState : JumpState
{
    private Slime slime;

    public Slime_JumpState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, D_JumpState stateData, Slime slime) : base(entity, stateMachine, animationBoolName, stateData)
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
        if (Time.time - startTime > 0.2f && entity.CheckGround())
        { 
            slime.stateMachine.ChangeState(slime.lookForPlayerState); 
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
