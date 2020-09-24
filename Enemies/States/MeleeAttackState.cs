using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackState : AttackState
{
    D_MeleeAttackState stateData;
    protected AttackDetails attackDetails;
    private LayerMask layerMask;

    public MeleeAttackState(Entity entity, FiniteStateMachine stateMachine, 
                           string animationBoolName, Transform attackPosition, D_MeleeAttackState stateData) : 
                           base(entity, stateMachine, animationBoolName, attackPosition)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        if (!entity.CheckFacingEnemy())
        {
            entity.Flip();
        }

        attackDetails.damageAmount = stateData.damage;
        attackDetails.position = entity.transform.position;

        if(entity.isFriendly)
        {
            layerMask = stateData.enemyLayerMask;
            attackDetails.knockbackX = stateData.friendlyKnockbackX;
            attackDetails.knockbackY = stateData.friendlyKnockbackY;
        }
        else
        {
            layerMask = stateData.playerLayerMask;
            attackDetails.knockbackX = stateData.knockbackX;
            attackDetails.knockbackY = stateData.knockbackY;
        }
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
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();

        Collider2D detectedObject = Physics2D.OverlapCircle(attackPosition.position, stateData.attackRadius, layerMask);
        if (detectedObject != null)
        {
            detectedObject.transform.SendMessage("Damage", attackDetails);
        }

        // Multihit
        //Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attackPosition.position,
        //                                                          stateData.attackRadius,
        //                                                          layerMask);
        //foreach (Collider2D collider in detectedObjects)
        //{
        //    collider.transform.SendMessage("Damage", attackDetails);
        //}
    }
}
