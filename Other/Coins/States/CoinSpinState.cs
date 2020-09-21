using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpinState : CoinState
{
    private float random;
    public CoinSpinState(Coin coin, CoinStateMachine stateMachine, CoinData coinData, string animationBoolName) : base(coin, stateMachine, coinData, animationBoolName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        random = Random.Range(0, coinData.spinDuration);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Time.time >= startTime + coinData.spinDuration + random)
        {
            stateMachine.ChangeState(coin.CoinIdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
