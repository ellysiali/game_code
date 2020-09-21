using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_IdleState : IdleState
{
    private Slime slime;

    /**************************************************************************
    Function: 	 Slime_MoveState
    Description: Slime_MoveState's constructor; initializes the starting variables
    Parameters:  entity            - the entity (mob) which uses the state
                 stateMachine      - the stateMachine which uses the state
                 animationBoolName - the name of the corresponding animation
                 stateData         - the data of the slime's MoveState
                 slime             - the specific slime which uses the state
    *************************************************************************/
    public Slime_IdleState(Entity entity, FiniteStateMachine stateMachine, 
                           string animationBoolName, D_IdleState stateData, Slime slime) : 
                           base(entity, stateMachine, animationBoolName, stateData)
    {
        this.slime = slime;
    }

    /**************************************************************************
    Function: 	 Enter
    Description: performs the necessary actions when entering the slime's 
                 idle state
    *************************************************************************/
    public override void Enter()
    {
        base.Enter();
    }

    /**************************************************************************
    Function: 	 Exit
    Description: performs the necessary actions when exiting the slime's 
                 move state
    *************************************************************************/
    public override void Exit()
    {
        base.Exit();
    }

    /**************************************************************************
    Function: 	 LogicUpdate
    Description: checks and updates any logic related details
    *************************************************************************/
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (entity.isFriendly && !slime.CheckPlayerInMaxRange())
        {
            stateMachine.ChangeState(slime.followState);
        }
        else if (isEnemyInMinAgroRange)
        {
            stateMachine.ChangeState(slime.playerDetectedState);
        }
        else if (isIdleTimeOver)
        {
            stateMachine.ChangeState(slime.moveState);
        }
    }

    /**************************************************************************
    Function: 	 PhysicsUpdate
    Description: checks and updates any physics related details
    *************************************************************************/
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
