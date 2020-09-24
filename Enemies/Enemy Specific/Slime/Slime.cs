using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;

public class Slime : Entity
{
    [SerializeField] protected D_IdleState idleStateData;
    [SerializeField] protected D_MoveState moveStateData;
    [SerializeField] protected D_ChargeState chargeStateData;
    [SerializeField] protected D_MeleeAttackState meleeAttackStateData;
    [SerializeField] protected D_StunState stunStateData;
    [SerializeField] protected D_DeadState deadStateData;
    [SerializeField] protected D_FollowState followStateData;

    public Slime_IdleState idleState { get; private set; }
    public Slime_MoveState moveState { get; private set; }
    public Slime_ChargeState chargeState { get; private set; }
    public Slime_MeleeAttackState meleeAttackState { get; private set; }
    public Slime_StunState stunState { get; private set; }
    public Slime_DeadState deadState { get; private set; }
    public Slime_FollowState followState { get; private set; }

    [SerializeField] private Transform meleeAttackPosition;

    public override void Start()
    {
        base.Start();
        idleState = new Slime_IdleState(this, stateMachine, "Idle", idleStateData, this);
        moveState = new Slime_MoveState(this, stateMachine, "Move", moveStateData, this);
        chargeState = new Slime_ChargeState(this, stateMachine, "Charge", chargeStateData, this);
        meleeAttackState = new Slime_MeleeAttackState(this, stateMachine, "MeleeAttack", meleeAttackPosition, meleeAttackStateData, this);
        stunState = new Slime_StunState(this, stateMachine, "Stun", stunStateData, this);
        deadState = new Slime_DeadState(this, stateMachine, "Dead", deadStateData, this);
        followState = new Slime_FollowState(this, stateMachine, "Move", followStateData, this);

        stateMachine.Initialize(moveState);
    }
    public override void Damage(AttackDetails attackDetails)
    {
        base.Damage(attackDetails);

        if (isDead)
        {
            stateMachine.ChangeState(deadState);
        }

        else if (isStunned && stateMachine.CurrentState != stunState)
        {
            stateMachine.ChangeState(stunState);
        }    
    }
    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        //// Melee range
        //Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttackStateData.attackRadius);
        //Gizmos.DrawWireSphere(transform.position, entityData.closeRangeActionDistance);

        //// Aggro range
        //Gizmos.DrawWireSphere(transform.position, entityData.maxAggroDistance);
        //Gizmos.DrawWireSphere(transform.position, entityData.minAggroDistance);
    }
}
