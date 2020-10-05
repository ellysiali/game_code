using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy_StunState : StunState
{
    private Dummy dummy;
    public Dummy_StunState(Entity entity, FiniteStateMachine stateMachine, 
                           string animationBoolName, Dummy dummy) : 
                           base(entity, stateMachine, animationBoolName)
    {
        this.dummy = dummy;
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
        if (isStunTimeOver)
        {
            stateMachine.ChangeState(dummy.idleState);
        }    
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
