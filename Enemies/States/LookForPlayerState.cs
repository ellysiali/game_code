using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookForPlayerState : State
{
    protected D_LookForPlayerState stateData;

    protected bool isPlayerinMinAgroRange, isAllTurnsDone, isAllTurnsTimeDone, turnImmediately;
    protected float lastTurnTime;
    protected int numTurnsDone;
    public LookForPlayerState(Entity entity, FiniteStateMachine stateMachine, 
                              string animationBoolName, D_LookForPlayerState stateData) : 
                              base(entity, stateMachine, animationBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isPlayerinMinAgroRange = entity.CheckEnemyInMinAgroRange();
    }

    public override void Enter()
    {
        base.Enter();
        isAllTurnsDone = isAllTurnsTimeDone = turnImmediately = false;
        lastTurnTime = startTime;
        numTurnsDone = 0;
        entity.SetVelocityX(0f);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (turnImmediately)
        {
            entity.Flip();
            lastTurnTime = Time.time;
            numTurnsDone++;
            turnImmediately = false;
        }

        else if (Time.time >= lastTurnTime + stateData.timeBetweenTurns && !isAllTurnsDone)
        {
            entity.Flip();
            lastTurnTime = Time.time;
            numTurnsDone++;
        }

        if (numTurnsDone >= stateData.totalNumOfTurns)
        {
            isAllTurnsDone = true;
        }

        if (Time.time >= lastTurnTime + stateData.timeBetweenTurns && isAllTurnsDone)
        {
            isAllTurnsTimeDone = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public void SetTurmImmediately(bool flip)
    {
        turnImmediately = flip;
    }
}
