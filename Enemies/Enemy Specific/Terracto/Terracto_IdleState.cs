using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terracto_IdleState : IdleState
{
    private Terracto terracto;
    public Terracto_IdleState(Entity entity, FiniteStateMachine stateMachine, 
                           string animationBoolName, D_IdleState stateData, Terracto terracto) : 
                           base(entity, stateMachine, animationBoolName, stateData)
    {
        this.terracto = terracto;
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
        if (entity.isFriendly && !terracto.CheckPlayerInMaxRange())
        {
            stateMachine.ChangeState(terracto.followState);
        }
        else if (entity.CheckEnemyInFarRangeAction() && entity.CheckCanAttack())
        {
            stateMachine.ChangeState(terracto.rangedAttackState);
        }
        else if (isIdleTimeOver)
        {
            stateMachine.ChangeState(terracto.moveState);
        }
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
