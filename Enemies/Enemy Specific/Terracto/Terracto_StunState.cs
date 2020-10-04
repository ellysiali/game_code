using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terracto_StunState : StunState
{
    private Terracto terracto;
    public Terracto_StunState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, Terracto terracto) : base(entity, stateMachine, animationBoolName)
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
        if (isStunTimeOver)
        {
            stateMachine.ChangeState(terracto.idleState);
        }    
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
