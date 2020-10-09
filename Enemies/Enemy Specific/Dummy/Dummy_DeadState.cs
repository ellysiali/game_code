using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy_DeadState : DeadState
{
    private Dummy dummy;
    private Vector2 vectorWorkspace;
    private GameObject objectWorkspace;

    public Dummy_DeadState(Entity entity, FiniteStateMachine stateMachine,
                           string animationBoolName, D_DeadState stateData, Dummy dummy) :
                           base(entity, stateMachine, animationBoolName, stateData)
    {
        this.dummy = dummy;
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
        objectWorkspace = Transform.Instantiate(dummy.head.gameObject);
        objectWorkspace.transform.position = entity.transform.position;
        vectorWorkspace.Set(0, Random.Range(0f, 5f));
        objectWorkspace.GetComponent<Rigidbody2D>().velocity = vectorWorkspace;
        objectWorkspace.transform.rotation = Quaternion.Euler(Vector3.forward * dummy.facingDirection * Random.Range(0f, 60f));

        objectWorkspace = Transform.Instantiate(dummy.body.gameObject);
        objectWorkspace.transform.position = entity.transform.position;
        vectorWorkspace.Set(0, Random.Range(5f, 10f));
        objectWorkspace.GetComponent<Rigidbody2D>().velocity = vectorWorkspace;
        objectWorkspace.transform.rotation = Quaternion.Euler(Vector3.forward * dummy.facingDirection * Random.Range(0f, 60f));

        entity.gameObject.SetActive(false);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        if (Time.time >= startTime + stateData.deadDuration)
        {
            stateMachine.ChangeState(dummy.idleState);
        }
    }
}
