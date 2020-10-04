using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terracto_DeadState : DeadState
{
    private Terracto terracto;
    public Terracto_DeadState(Entity entity, FiniteStateMachine stateMachine, 
                           string animationBoolName, D_DeadState stateData, Terracto terracto) : 
                           base(entity, stateMachine, animationBoolName, stateData)
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
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        if (Time.time >= startTime + stateData.deadDuration)
        {
            stateMachine.ChangeState(terracto.moveState);
        }
    }
}
