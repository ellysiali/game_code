using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_StunState : StunState
{
    private Slime slime;
    public Slime_StunState(Entity entity, FiniteStateMachine stateMachine, 
                           string animationBoolName, Slime slime) : 
                           base(entity, stateMachine, animationBoolName)
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
        if (isStunTimeOver)
        {
            if (performCloseRangeAction)
            {
                stateMachine.ChangeState(slime.meleeAttackState);
            }

            else if (isPlayerInMinAggroRange)
            {
                stateMachine.ChangeState(slime.chargeState);
            }

            else
            {
                stateMachine.ChangeState(slime.idleState);
            }
        }    
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
