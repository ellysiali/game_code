using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinFallState : CoinState
{
    public CoinFallState(Coin coin, CoinStateMachine stateMachine, CoinData coinData, string animationBoolName) : base(coin, stateMachine, coinData, animationBoolName)
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
        coin.SetVelocityX(Random.Range(-coinData.initialVelocityX, coinData.initialVelocityX));
        coin.SetVelocityY(Random.Range(0, coinData.initialVelocityY));
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (coin.CheckIfGrounded)
        {
            stateMachine.ChangeState(coin.CoinSpinState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
