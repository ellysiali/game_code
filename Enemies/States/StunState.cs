using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class StunState : State
{
    const float StunTime = 0.5f;

    protected bool isStunTimeOver, performCloseRangeAction, isPlayerInMinAggroRange;

    public StunState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName) :base(entity, stateMachine, animationBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
        performCloseRangeAction = entity.CheckEnemyInCloseRangeAction();
        isPlayerInMinAggroRange = entity.CheckEnemyInMinAggroRange();
    }

    public override void Enter()
    {
        base.Enter();
        isStunTimeOver = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Time.time >= startTime + StunTime)
        {
            isStunTimeOver = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        if (entity.rigidBody.velocity.y <= 0)
        {
            entity.SetVelocityX(0);
        }
    }

}
