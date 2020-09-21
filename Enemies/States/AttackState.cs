using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    protected Transform attackPosition;
    protected bool isAnimationFinished, isPlayerInMinAgroRange;
    public AttackState(Entity entity, FiniteStateMachine stateMachine, 
                       string animationBoolName, Transform attackPosition) : 
                       base(entity, stateMachine, animationBoolName)
    {
        this.attackPosition = attackPosition;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isPlayerInMinAgroRange = entity.CheckEnemyInMinAgroRange();
    }

    public override void Enter()
    {
        isAnimationFinished = false;
        entity.atsm.attackState = this;
        entity.SetVelocityX(0f);
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

    public virtual void TriggerAttack()
    {

    }

    public virtual void FinishAttack()
    {
        isAnimationFinished = true;
    }
}
