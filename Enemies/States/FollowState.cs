using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowState : State
{
    D_FollowState stateData;

    public bool isByPlayer, isFollowDurationOver;

    public FollowState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, D_FollowState stateData) : base(entity, stateMachine, animationBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isByPlayer = entity.CheckPlayerInMinRange();

        if (!entity.CheckFacingPlayer())
        {
            entity.Flip();
        }
        entity.SetVelocityX(stateData.movementSpeed);
        if ((!entity.CheckLedge() || entity.CheckWall()) && entity.CheckGround())
        {
            entity.SetVelocityY(stateData.jumpVelocity);
        }
    }

    public override void Enter()
    {
        base.Enter();
        isByPlayer = false;
        isFollowDurationOver = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Time.time >= startTime + stateData.followDuration)
        {
            isFollowDurationOver = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
