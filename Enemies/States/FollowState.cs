using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowState : State
{
    D_FollowState stateData;

    static float FlipCooldown = 1f;
    protected bool isInMinPlayerRange, isInMaxPlayerRange, outOfRange;
    protected float lastInMaxPlayerRange;
    private float lastFlipTime = -100f;

    public FollowState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, D_FollowState stateData) : base(entity, stateMachine, animationBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isInMinPlayerRange = entity.CheckPlayerInMinRange();
        isInMaxPlayerRange = entity.CheckPlayerInMaxRange();

        if(isInMaxPlayerRange)
        {
            lastInMaxPlayerRange = Time.time;
        }    

        if (!entity.CheckFacingPlayer() && Time.time >= lastFlipTime + FlipCooldown)
        {
            entity.Flip();
            lastFlipTime = Time.time;
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
        lastInMaxPlayerRange = startTime;
        isInMinPlayerRange = false;
        isInMaxPlayerRange = false;
        outOfRange = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Time.time >= lastInMaxPlayerRange + stateData.followDuration)
        {
            outOfRange = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
