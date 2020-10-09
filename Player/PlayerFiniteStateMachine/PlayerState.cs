using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected Player player;
    protected PlayerStateMachine stateMachine;
    protected PlayerData playerData;
    protected float startTime;
    protected bool isAnimationFinished, isGamePaused;

    private bool pauseInput;
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
        isGamePaused = false;
    }

    public virtual void Exit()
    {
        player.Anim.SetBool(animationBoolName, false);
    }

    public virtual void LogicUpdate() 
    {
        pauseInput = player.InputHandler.PauseInput;

        if (pauseInput)
        {
            player.InputHandler.UsePauseInput();
            if (Time.timeScale == 1.0f)
            {
                player.menu.SetActive(true);
                Time.timeScale = 0.0f;
                isGamePaused = true;
            }
            else
            {
                player.menu.SetActive(false);
                Time.timeScale = 1.0f;
                isGamePaused = false;
            }
        }
    }

    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }
    
    public virtual void AnimationTrigger() { }

    public virtual void AnimationFinishTrigger() => isAnimationFinished = true;
}
