using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerAbilityState
{
    public PlayerDashState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName) : base(player, stateMachine, playerData, animationBoolName)
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
        player.GetComponent<CapsuleCollider2D>().size = new Vector2(2, 0.9f);
        player.GetComponent<CapsuleCollider2D>().offset = new Vector2(0, -0.8f);
        player.GetComponent<CapsuleCollider2D>().direction = CapsuleDirection2D.Horizontal;
    }

    public override void Exit()
    {
        base.Exit();
        player.lastDash = Time.time;
        player.GetComponent<CapsuleCollider2D>().size = new Vector2(0.85f, 2.5f);
        player.GetComponent<CapsuleCollider2D>().offset = new Vector2(0, 0);
        player.GetComponent<CapsuleCollider2D>().direction = CapsuleDirection2D.Vertical;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Time.time >= startTime + playerData.dashDuration)
        {
            isAbilityDone = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        player.SetVelocityX(playerData.dashVelocity * player.facingDirection);
    }
}
