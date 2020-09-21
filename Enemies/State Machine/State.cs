using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
    protected FiniteStateMachine stateMachine;
    protected Entity entity;

    protected float startTime;
    protected string animationBoolName;

    /**************************************************************************
    Function: 	 State
    Description: State's constructor; initializes the Entity and 
                 FiniteStateMachine variables
    Parameters:  entity            - the entity (mob) which uses the state
                 stateMachine      - the stateMachine which uses the state
                 animationBoolName - the name of the corresponding animation
    *************************************************************************/
    public State(Entity entity, FiniteStateMachine stateMachine, string animationBoolName)
    {
        this.entity = entity;
        this.stateMachine = stateMachine;
        this.animationBoolName = animationBoolName;
    }

    /**************************************************************************
    Function: 	 enter
    Description: performs the necessary actions when entering the given state
    *************************************************************************/
    public virtual void Enter()
    {
        DoChecks();
        startTime = Time.time;
        entity.animator.SetBool(animationBoolName, true);
    }

    /**************************************************************************
    Function: 	 exit
    Description: performs the necessary actions when exiting the given state
    *************************************************************************/
    public virtual void Exit()
    {
        entity.animator.SetBool(animationBoolName, false);

    }

    /**************************************************************************
    Function: 	 logicUpdate
    Description: checks and updates any logic-related details
    *************************************************************************/
    public virtual void LogicUpdate()
    {

    }

    /**************************************************************************
    Function: 	 physicsUpdate
    Description: checks and updates any physics related details
    *************************************************************************/
    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }

    /**************************************************************************
    Function: 	 DoChecks
    Description: perform the given checks in both Enter() and PhysicsUpdate()
    *************************************************************************/
    public virtual void DoChecks()
    {

    }
}
