using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : State
{
    protected D_MoveState stateData;

    protected float moveTime;
    protected bool isDetectingWall, isDetectingLedge, isMoveTimeOver,
                   isPlayerInMinAgroRange;

    /**************************************************************************
    Function: 	 MoveState
    Description: MoveState's constructor; initializes the starting variables
    Parameters:  entity            - the entity (mob) which uses the state
                 stateMachine      - the stateMachine which uses the state
                 animationBoolName - the name of the corresponding animation
                 stateData         - the data of the MoveState
    *************************************************************************/
    public MoveState(Entity entity, FiniteStateMachine stateMachine, 
                     string animationBoolName, D_MoveState stateData) : 
           base(entity, stateMachine, animationBoolName)
    {
        this.stateData = stateData;
    }

    /**************************************************************************
    Function: 	 Enter
    Description: performs the necessary actions when entering the move state
    *************************************************************************/
    public override void Enter()
    {
        base.Enter();
        setRandomMoveTime();
        isMoveTimeOver = false;
    }

    /**************************************************************************
    Function: 	 Exit
    Description: performs the necessary actions when exiting the move state
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
        if (isDetectingWall || !isDetectingLedge)
        {
            entity.Flip();
        }
        if (Time.time >= startTime + moveTime)
        {
            isMoveTimeOver = true;
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

    /**************************************************************************
    Function: 	 DoChecks
    Description: perform the given checks in both Enter() and PhysicsUpdate()
    *************************************************************************/
    public override void DoChecks()
    {
        entity.SetVelocityX(stateData.movementSpeed);
        isDetectingLedge = entity.CheckLedge();
        isDetectingWall = entity.CheckWall();
        isPlayerInMinAgroRange = entity.CheckEnemyInMinAgroRange();
    }

    /**************************************************************************
    Function: 	 setRandomMoveTime
    Description: set a random move time for the entity
    *************************************************************************/
    private void setRandomMoveTime()
    {
        moveTime = Random.Range(stateData.minMoveTime, stateData.maxMoveTime);
    }
}
