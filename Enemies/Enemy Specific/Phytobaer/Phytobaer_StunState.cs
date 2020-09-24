using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phytobaer_StunState : StunState
{
    private Phytobaer phytobaer;
    public Phytobaer_StunState(Entity entity, FiniteStateMachine stateMachine, 
                           string animationBoolName, D_StunState stateData, Phytobaer phytobaer) : 
                           base(entity, stateMachine, animationBoolName, stateData)
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

            else if (isPlayerInMinAgroRange)
            {
                stateMachine.ChangeState(phytobaer.chargeState);
            }

            else
            {
                phytobaer.lookForPlayerState.SetTurmImmediately(true);
                stateMachine.ChangeState(phytobaer.lookForPlayerState);
            }
        }    
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
