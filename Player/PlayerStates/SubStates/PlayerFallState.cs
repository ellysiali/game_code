using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallState : PlayerState
{
    private bool isGrounded, attackInput;
    public PlayerFallState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName) : base(player, stateMachine, playerData, animationBoolName)
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
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
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
}
