using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phytobaer_LookForPlayerState : LookForPlayerState
{
    private Phytobaer phytobaer;
    public Phytobaer_LookForPlayerState(Entity entity, FiniteStateMachine stateMachine, 
                                    string animationBoolName, D_LookForPlayerState stateData, Phytobaer phytobaer) : 
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
        if (entity.isFriendly && !phytobaer.CheckPlayerInMaxRange())
        {
            stateMachine.ChangeState(phytobaer.followState);
        }
        else if (isPlayerinMinAgroRange)
        {
            stateMachine.ChangeState(phytobaer.playerDetectedState);
        }

        else if (isAllTurnsTimeDone)
        {
            stateMachine.ChangeState(phytobaer.moveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override string ToString()
    {
        return base.ToString();
    }
}
