using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttackState : AttackState
{
    D_RangedAttackState stateData;
    private AttackDetails attackDetails;
    private LayerMask enemyLayerMask;
    private GameObject workspace;

    public RangedAttackState(Entity entity, FiniteStateMachine stateMachine, 
                           string animationBoolName, Transform attackPosition, D_RangedAttackState stateData) : 
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

        if (entity.isFriendly)
        {
            enemyLayerMask = stateData.enemyLayerMask;
            attackDetails.knockbackX = stateData.friendlyKnockbackX;
            attackDetails.knockbackY = stateData.friendlyKnockbackY;
        }
        else
        {
            enemyLayerMask = stateData.playerLayerMask;
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
        workspace = Transform.Instantiate(stateData.projectile);
        workspace.GetComponent<Projectile>().Initialize(attackDetails, entity.facingDirection * stateData.projectileSpeed, enemyLayerMask, entity.isFriendly);
        workspace.transform.position = attackPosition.position;
    }
}
