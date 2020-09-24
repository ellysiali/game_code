using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    protected D_IdleState stateData;
    protected float idleTime;
    protected bool isIdleTimeOver, flipAfterIdle, isEnemyInMinAggroRange;

    /**************************************************************************
    Function: 	 IdleState
    Description: IdleState's constructor; initializes the starting variables
    Parameters:  entity            - the entity (mob) which uses the state
                 stateMachine      - the stateMachine which uses the state
                 animationBoolName - the name of the corresponding animation
                 stateData         - the data of the IdleState
    *************************************************************************/
    public IdleState(Entity entity, FiniteStateMachine stateMachine, 
                     string animationBoolName, D_IdleState stateData) : 
                     base(entity, stateMachine, animationBoolName)
    {
        this.stateData = stateData;
    }

    /**************************************************************************
    Function: 	 Enter
    Description: performs the necessary actions when entering the idle state
    *************************************************************************/
    public override void Enter()
    {
        base.Enter();
        entity.SetVelocityX(0f);
        isIdleTimeOver = false;
        setRandomIdleTime();
    }

    /**************************************************************************
    Function: 	 Exit
    Description: performs the necessary actions when exiting the idle state
    *************************************************************************/
    public override void Exit()
    {
        base.Exit();
        SetFlipAfterIdle(Random.value > 0.5f);
        if (flipAfterIdle)
        {
            entity.Flip();
        }
    }

    /**************************************************************************
    Function: 	 LogicUpdate
    Description: checks and updates any logic related details
    *************************************************************************/
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Time.time >= startTime + idleTime)
        {
            isIdleTimeOver = true;
        }
    }

    /**************************************************************************
    Function: 	 PhysicsUpdate
    Description: checks and updates any physics related details
    Parameters:  flip - true if should flip; false otherwise
    *************************************************************************/
    public void SetFlipAfterIdle(bool flip)
    {
        flipAfterIdle = flip;
    }

    /**************************************************************************
    Function: 	 DoChecks
    Description: perform the given checks in both Enter() and PhysicsUpdate()
    *************************************************************************/
    public override void DoChecks()
    {
        isEnemyInMinAggroRange = entity.CheckEnemyInMinAggroRange();
    }

    /**************************************************************************
    Function: 	 setRandomIdleTime
    Description: set a random idle time for the entity
    *************************************************************************/
    private void setRandomIdleTime()
    {
        idleTime = Random.Range(stateData.minIdleTime, stateData.maxIdleTime);
    }
}
