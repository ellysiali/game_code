using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terracto_RangedAttackState : RangedAttackState
{
    private Terracto terracto;
    public Terracto_RangedAttackState(Entity entity, FiniteStateMachine stateMachine, 
                                  string animationBoolName, Transform attackPosition, 
                                  D_RangedAttackState stateData, Terracto terracto) : 
                                  base(entity, stateMachine, animationBoolName, attackPosition, stateData)
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
    public override void FinishAttack()
    {
        base.FinishAttack();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isAnimationFinished)
        {
            stateMachine.ChangeState(terracto.idleState);
        }
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    public override void TriggerAttack()
    {
        base.TriggerAttack();
    }
}
