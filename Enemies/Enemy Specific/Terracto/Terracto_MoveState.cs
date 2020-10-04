using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terracto_MoveState : MoveState
{
    private Terracto terracto;

    public Terracto_MoveState(Entity entity, FiniteStateMachine stateMachine, 
                           string animationBoolName, D_MoveState stateData, Terracto terracto) : 
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
        else if (isMoveTimeOver)
        {
            stateMachine.ChangeState(terracto.idleState);
        }
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
