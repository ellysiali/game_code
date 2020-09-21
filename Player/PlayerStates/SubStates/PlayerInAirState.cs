using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerState
{
    private bool isGrounded, isJumping, jumpInputStop, attackInput;
    public PlayerInAirState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName) : base(player, stateMachine, playerData, animationBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isGrounded = player.CheckIfGrounded();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        jumpInputStop = player.InputHandler.JumpInputStop;

        attackInput = player.InputHandler.AttackInput;

        if (attackInput)
        {
            player.InputHandler.UseAttackInput();
            if (player.CheckIfCanAttack())
            {
                stateMachine.ChangeState(player.Attack1State);
            }
        }

        if (isGrounded && player.currentVelocity.y < 0.01f)
        {
            stateMachine.ChangeState(player.IdleState);
        }

        player.Anim.SetFloat("yVelocity", player.currentVelocity.y);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        CheckJumpHeightMultiplier();
        Vector2 input = player.InputHandler.MovementInput;
        player.SetVelocityX(playerData.movementVelocity * input.x);
        if (input.x > 0)
        {
            player.CheckIfShouldFlip(1);
        }
        else if (input.x < 0)
        {
            player.CheckIfShouldFlip(-1);
        }
    }

    private void CheckJumpHeightMultiplier()
    {
        if (isJumping)
        {
            if (jumpInputStop && Time.time >= startTime + playerData.variableJumpDelay)
            {
                player.SetVelocityY(player.currentVelocity.y * playerData.variableJumpHeightMultiplier);
                isJumping = false;
            }
            else if (player.currentVelocity.y <= 0)
            {
                isJumping = false;
            }
        }
    }

    public void SetIsJumping() => isJumping = true;
}
