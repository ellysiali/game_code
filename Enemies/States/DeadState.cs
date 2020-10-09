using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class DeadState : State
{
    private float coinDrop, copperDrop, silverDrop, goldDrop;
    private GameObject workspace;

    protected D_DeadState stateData;
    public DeadState(Entity entity, FiniteStateMachine stateMachine, 
                     string animationBoolName, D_DeadState stateData) : 
                     base(entity, stateMachine, animationBoolName)
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
        coinDrop = Mathf.RoundToInt(Random.Range(stateData.averageCoinDrops - stateData.coinRange, stateData.averageCoinDrops + stateData.coinRange));
        goldDrop = (int) (coinDrop / stateData.gold.GetComponent<Coin>().CheckValue());
        silverDrop = (int) ((coinDrop % stateData.gold.GetComponent<Coin>().CheckValue()) / stateData.silver.GetComponent<Coin>().CheckValue());
        copperDrop = (int) ((coinDrop % stateData.gold.GetComponent<Coin>().CheckValue()) % stateData.silver.GetComponent<Coin>().CheckValue());
    }

    public override void Exit()
    {
        base.Exit();
        stateData.deathParticle.transform.position = entity.transform.position;
        Transform.Instantiate(stateData.deathParticle);
        for (int i = 0; i < goldDrop; i++)
        {
            workspace = Transform.Instantiate(stateData.gold);
            workspace.transform.position = entity.transform.position;
        }
        for (int i = 0; i < silverDrop; i++)
        {
            workspace = Transform.Instantiate(stateData.silver);
            workspace.transform.position = entity.transform.position;
        }
        for (int i = 0; i < copperDrop; i++)
        {
            workspace = Transform.Instantiate(stateData.copper);
            workspace.transform.position = entity.transform.position;
        }
        entity.gameObject.SetActive(false);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
