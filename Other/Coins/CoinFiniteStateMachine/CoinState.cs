using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinState
{
    protected Coin coin;
    protected CoinStateMachine stateMachine;
    protected CoinData coinData;
    protected float startTime;
    protected bool isAnimationFinished;

    private string animationBoolName;

    public CoinState(Coin coin, CoinStateMachine stateMachine, CoinData coinData, string animationBoolName)
    {
        this.coin = coin;
        this.stateMachine = stateMachine;
        this.coinData = coinData;
        this.animationBoolName = animationBoolName;
    }

    public virtual void DoChecks() { }

    public virtual void Enter()
    {
        DoChecks();
        startTime = Time.time;
        coin.Anim.SetBool(animationBoolName, true);
        isAnimationFinished = false;
    }

    public virtual void Exit()
    {
        coin.Anim.SetBool(animationBoolName, false);
    }

    public virtual void LogicUpdate() { }

    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }

    public virtual void AnimationTrigger() { }

    public virtual void AnimationFinishTrigger() => isAnimationFinished = true;
}
