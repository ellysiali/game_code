using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;

public class Terracto : Entity
{
    [SerializeField] protected D_IdleState idleStateData;
    [SerializeField] protected D_MoveState moveStateData;
    [SerializeField] protected D_RangedAttackState rangedAttackStateData;
    [SerializeField] protected D_DeadState deadStateData;
    [SerializeField] protected D_FollowState followStateData;

    public Terracto_IdleState idleState { get; private set; }
    public Terracto_MoveState moveState { get; private set; }
    public Terracto_RangedAttackState rangedAttackState { get; private set; }
    public Terracto_StunState stunState { get; private set; }
    public Terracto_DeadState deadState { get; private set; }
    public Terracto_FollowState followState { get; private set; }
    public Terracto_TeleportState teleportState { get; private set; }

    [SerializeField] private Transform rangedAttackPosition;

    public override void Start()
    {
        base.Start();
        idleState = new Terracto_IdleState(this, stateMachine, "Idle", idleStateData, this);
        moveState = new Terracto_MoveState(this, stateMachine, "Move", moveStateData, this);
        rangedAttackState = new Terracto_RangedAttackState(this, stateMachine, "RangedAttack", rangedAttackPosition, rangedAttackStateData, this);
        stunState = new Terracto_StunState(this, stateMachine, "Stun", this);
        deadState = new Terracto_DeadState(this, stateMachine, "Dead", deadStateData, this);
        followState = new Terracto_FollowState(this, stateMachine, "Move", followStateData, this);
        teleportState = new Terracto_TeleportState(this, stateMachine, "Teleport", this);

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

        // Attack range
        //Gizmos.DrawLine(transform.position, transform.position + Vector3.right * facingDirection * entityData.farRangeActionDistance);
    }
}
