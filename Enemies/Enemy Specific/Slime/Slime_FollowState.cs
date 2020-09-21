using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_FollowState : FollowState
{
    private Slime slime;
    public Slime_FollowState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, D_FollowState stateData, Slime slime) : base(entity, stateMachine, animationBoolName, stateData)
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
        if (isByPlayer)
        {
            stateMachine.ChangeState(slime.idleState);
        }
        else if (!isByPlayer && isFollowDurationOver)
        {
            slime.transform.position = slime.CheckPlayerPosition();
            stateMachine.ChangeState(slime.idleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
