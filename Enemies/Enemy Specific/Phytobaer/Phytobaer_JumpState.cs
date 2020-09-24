using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phytobaer_JumpState : JumpState
{
    private Phytobaer phytobaer;

    public Phytobaer_JumpState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, D_JumpState stateData, Phytobaer phytobaer) : base(entity, stateMachine, animationBoolName, stateData)
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
        if (Time.time - startTime > 0.2f && entity.CheckGround())
        { 
            phytobaer.stateMachine.ChangeState(phytobaer.lookForPlayerState); 
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
