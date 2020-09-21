using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_DeadState : DeadState
{
    private Slime slime;
    public Slime_DeadState(Entity entity, FiniteStateMachine stateMachine, 
                           string animationBoolName, D_DeadState stateData, Slime slime) : 
                           base(entity, stateMachine, animationBoolName, stateData)
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
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        if (Time.time >= startTime + stateData.deadDuration)
        {
            stateMachine.ChangeState(slime.moveState);
        }
    }
}
