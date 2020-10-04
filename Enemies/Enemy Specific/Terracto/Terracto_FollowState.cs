using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terracto_FollowState : FollowState
{
    private Terracto terracto;
    public Terracto_FollowState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, D_FollowState stateData, Terracto terracto) : base(entity, stateMachine, animationBoolName, stateData)
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
        if (isInMinPlayerRange)
        {
            stateMachine.ChangeState(terracto.idleState);
        }
        else if (!isInMinPlayerRange && outOfRange)
        {
            stateMachine.ChangeState(terracto.teleportState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
