using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinIdleState : CoinState
{
    private float timeUnilFlip, flipTime = Time.time, timeUnilJump, jumpTime = Time.time;
    private bool isGrounded;
    public CoinIdleState(Coin coin, CoinStateMachine stateMachine, CoinData coinData, string animationBoolName) : base(coin, stateMachine, coinData, animationBoolName)
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
        isGrounded = coin.CheckIfGrounded;
    }

    public override void Enter()
    {
        base.Enter();
        SetRandomFlipTime();
        SetRandomJumpTime();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(Time.time >= jumpTime + timeUnilJump && isGrounded)
        {
            coin.SetVelocityX(coin.CheckFacingDirection * coinData.jumpX);
            coin.SetVelocityY(coinData.jumpY);
            jumpTime = Time.time;
        }

        if (Time.time >= flipTime + timeUnilFlip && isGrounded)
        {
            SetRandomFlipTime();
            coin.Flip();
            flipTime = Time.time;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private void SetRandomFlipTime()
    {
        timeUnilFlip = Random.Range(coinData.minTimeUntilFlip, coinData.maxTimeUntilFlip);
    }

    private void SetRandomJumpTime()
    {
        timeUnilJump = Random.Range(coinData.minTimeUntilJump, coinData.maxTimeUntilJump);
    }
}
