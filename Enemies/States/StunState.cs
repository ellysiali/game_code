using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class StunState : State
{
    protected D_StunState stateData;

    protected bool isStunTimeOver, isGrounded, isMovementStopped,
                   performCloseRangeAction, isPlayerInMinAgroRange;

    public StunState(Entity entity, FiniteStateMachine stateMachine,
                       string animationBoolName, D_StunState stateData) :
                       base(entity, stateMachine, animationBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isGrounded = entity.CheckGround();
        performCloseRangeAction = entity.CheckEnemyInCloseRangeAction();
        isPlayerInMinAgroRange = entity.CheckEnemyInMinAgroRange();
    }

    public override void Enter()
    {
        base.Enter();
        isStunTimeOver = false;
        isMovementStopped = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Time.time >= startTime + stateData.stunTime)
        {
            isStunTimeOver = true;
        }

        if(isGrounded && Time.time >= startTime + stateData.stunTime && !isMovementStopped)
        {
            entity.SetVelocityX(0);
            isMovementStopped = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

}
