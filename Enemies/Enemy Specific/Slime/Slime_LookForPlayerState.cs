using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_LookForPlayerState : LookForPlayerState
{
    private Slime slime;
    public Slime_LookForPlayerState(Entity entity, FiniteStateMachine stateMachine, 
                                    string animationBoolName, D_LookForPlayerState stateData, Slime slime) : 
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
        if (entity.isFriendly && !slime.CheckPlayerInMaxRange())
        {
            stateMachine.ChangeState(slime.followState);
        }
        else if (isPlayerinMinAgroRange)
        {
            stateMachine.ChangeState(slime.playerDetectedState);
        }

        else if (isAllTurnsTimeDone)
        {
            stateMachine.ChangeState(slime.moveState);
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
