using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;

public class Phytobaer : Entity
{
    [SerializeField] protected D_IdleState idleStateData;
    [SerializeField] protected D_MoveState moveStateData;
    [SerializeField] protected D_PlayerDetectedState playerDetectedStateData;
    [SerializeField] protected D_ChargeState chargeStateData;
    [SerializeField] protected D_LookForPlayerState lookForPlayerStateData;
    [SerializeField] protected D_MeleeAttackState meleeAttackStateData;
    [SerializeField] protected D_StunState stunStateData;
    [SerializeField] protected D_DeadState deadStateData;
    [SerializeField] protected D_JumpState jumpStateData;
    [SerializeField] protected D_FollowState followStateData;

    public Phytobaer_IdleState idleState { get; private set; }
    public Phytobaer_MoveState moveState { get; private set; }
    public Phytobaer_PlayerDetectedState playerDetectedState { get; private set; }
    public Phytobaer_ChargeState chargeState { get; private set; }
    public Phytobaer_LookForPlayerState lookForPlayerState { get; private set; }
    public Phytobaer_MeleeAttackState meleeAttackState { get; private set; }
    public Phytobaer_StunState stunState { get; private set; }
    public Phytobaer_DeadState deadState { get; private set; }
    public Phytobaer_JumpState jumpState { get; private set; }
    public Phytobaer_FollowState followState { get; private set; }

    [SerializeField] private Transform meleeAttackPosition;

    public override void Start()
    {
        base.Start();
        idleState = new Phytobaer_IdleState(this, stateMachine, "Idle", idleStateData, this);
        moveState = new Phytobaer_MoveState(this, stateMachine, "Move", moveStateData, this);
        playerDetectedState = new Phytobaer_PlayerDetectedState(this, stateMachine, "PlayerDetected", playerDetectedStateData, this);
        chargeState = new Phytobaer_ChargeState(this, stateMachine, "Charge", chargeStateData, this);
        lookForPlayerState = new Phytobaer_LookForPlayerState(this, stateMachine, "LookForPlayer", lookForPlayerStateData, this);
        meleeAttackState = new Phytobaer_MeleeAttackState(this, stateMachine, "MeleeAttack", meleeAttackPosition, meleeAttackStateData, this);
        stunState = new Phytobaer_StunState(this, stateMachine, "Stun", stunStateData, this);
        deadState = new Phytobaer_DeadState(this, stateMachine, "Dead", deadStateData, this);
        jumpState = new Phytobaer_JumpState(this, stateMachine, "Move", jumpStateData, this);
        followState = new Phytobaer_FollowState(this, stateMachine, "Move", followStateData, this);

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
    //public override void OnDrawGizmos()
    //{
    //    base.OnDrawGizmos();

    //    // Melee range
    //    //Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttackStateData.attackRadius);
    //    //Gizmos.DrawWireSphere(transform.position, entityData.closeRangeActionDistance);

    //    // Agro range
    //    //Gizmos.DrawWireSphere(transform.position, entityData.maxAgroDistance);
    //    //Gizmos.DrawWireSphere(transform.position, entityData.minAgroDistance);
    //}
}
