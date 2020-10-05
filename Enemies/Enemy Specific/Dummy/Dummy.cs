using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;

public class Dummy : Entity
{
    [SerializeField] protected D_IdleState idleStateData;
    [SerializeField] protected D_DeadState deadStateData;

    public Dummy_IdleState idleState { get; private set; }
    public Dummy_StunState stunState { get; private set; }
    public Dummy_DeadState deadState { get; private set; }

    [SerializeField] private Transform meleeAttackPosition;
    [SerializeField] public Transform head, body;


    public override void Start()
    {
        base.Start();
        idleState = new Dummy_IdleState(this, stateMachine, "Idle", idleStateData, this);
        stunState = new Dummy_StunState(this, stateMachine, "Stun", this);
        deadState = new Dummy_DeadState(this, stateMachine, "Dead", deadStateData, this);

        stateMachine.Initialize(idleState);
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

    public override void ApplyTouchDamage()
    {
    }
}
