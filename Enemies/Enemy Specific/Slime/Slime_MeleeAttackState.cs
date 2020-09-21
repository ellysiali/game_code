using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_MeleeAttackState : MeleeAttackState
{
    private Slime slime;

    public Slime_MeleeAttackState(Entity entity, FiniteStateMachine stateMachine, 
                                  string animationBoolName, Transform attackPosition, 
                                  D_MeleeAttackState stateData, Slime slime) : 
                                  base(entity, stateMachine, animationBoolName, attackPosition, stateData)
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

    public override void FinishAttack()
    {
        base.FinishAttack();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isAnimationFinished)
        {
         if (isPlayerInMinAgroRange)
            {
                stateMachine.ChangeState(slime.playerDetectedState);
            }
            else
            {
                stateMachine.ChangeState(slime.lookForPlayerState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();
    }
}
