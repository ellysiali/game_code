using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_ChargeState : ChargeState
{
    private Slime slime;
    public Slime_ChargeState(Entity entity, FiniteStateMachine stateMachine, 
                             string animationBoolName, D_ChargeState stateData, Slime slime) : 
                             base(entity, stateMachine, animationBoolName, stateData)
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

        if (performCloseRangeAction && slime.CheckCanAttack())
        {
            stateMachine.ChangeState(slime.meleeAttackState);
        }

        else if (!isEnemyInMaxAggroRange)
        {
            stateMachine.ChangeState(slime.idleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
