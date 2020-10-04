using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class StunState : State
{
    private float stunTime = 0.5f;

    protected bool isStunTimeOver, isGrounded, isMovementStopped,
                   performCloseRangeAction, isPlayerInMinAggroRange;

    public StunState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName) :base(entity, stateMachine, animationBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isGrounded = entity.CheckGround();
        performCloseRangeAction = entity.CheckEnemyInCloseRangeAction();
        isPlayerInMinAggroRange = entity.CheckEnemyInMinAggroRange();
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
        if (Time.time >= startTime + stunTime)
        {
            isStunTimeOver = true;
        }

        if(isGrounded && Time.time >= startTime + stunTime && !isMovementStopped)
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
