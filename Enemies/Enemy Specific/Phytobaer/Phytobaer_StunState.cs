using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phytobaer_StunState : StunState
{
    private Phytobaer phytobaer;
    public Phytobaer_StunState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, Phytobaer phytobaer) : base(entity, stateMachine, animationBoolName)
    {
        this.phytobaer = phytobaer;
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
                stateMachine.ChangeState(phytobaer.meleeAttackState);
            }

            else if (isPlayerInMinAggroRange)
            {
                stateMachine.ChangeState(phytobaer.chargeState);
            }

            else
            {
                stateMachine.ChangeState(phytobaer.idleState);
            }
        }    
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
