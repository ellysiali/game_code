using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phytobaer_DeadState : DeadState
{
    private Phytobaer phytobaer;
    public Phytobaer_DeadState(Entity entity, FiniteStateMachine stateMachine, 
                           string animationBoolName, D_DeadState stateData, Phytobaer phytobaer) : 
                           base(entity, stateMachine, animationBoolName, stateData)
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
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        if (Time.time >= startTime + stateData.deadDuration)
        {
            stateMachine.ChangeState(phytobaer.moveState);
        }
    }
}
