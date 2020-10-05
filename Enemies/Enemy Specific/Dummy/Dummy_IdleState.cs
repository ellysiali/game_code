using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy_IdleState : IdleState
{
    private Dummy dummy;

    public Dummy_IdleState(Entity entity, FiniteStateMachine stateMachine, 
                           string animationBoolName, D_IdleState stateData, Dummy dummy) : 
                           base(entity, stateMachine, animationBoolName, stateData)
    {
        this.dummy = dummy;
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
    }
}
