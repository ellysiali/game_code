using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected Player player;
    protected PlayerStateMachine stateMachine;
    protected PlayerData playerData;
    protected float startTime;
    protected bool isAnimationFinished, menuInput;

    private string animationBoolName;

    public PlayerState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.playerData = playerData;
        this.animationBoolName = animationBoolName;
    }

    public virtual void DoChecks() { }

    public virtual void Enter()
    {
        DoChecks();
        startTime = Time.time;
        player.Anim.SetBool(animationBoolName, true);
        isAnimationFinished = false;
    }

    public virtual void Exit()
    {
        player.Anim.SetBool(animationBoolName, false);
    }

    public virtual void LogicUpdate() 
    {
        menuInput = player.InputHandler.MenuInput;
    }

    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }
    
    public virtual void AnimationTrigger() { }

    public virtual void AnimationFinishTrigger() => isAnimationFinished = true;
}
