using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected Vector2 input;
    private bool jumpInput, isGrounded, dashInput, attackInput, inventoryInput;

    public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName) : base(player, stateMachine, playerData, animationBoolName)
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
        if (!isGamePaused)
        {
            input = player.InputHandler.MovementInput;
            jumpInput = player.InputHandler.JumpInput;
            dashInput = player.InputHandler.DashInput;
            attackInput = player.InputHandler.AttackInput;
            inventoryInput = player.InputHandler.InventoryInput;
        }

        if (attackInput)
        {
            player.InputHandler.UseAttackInput();
            if (player.isInDialogueRange)
            {
                stateMachine.ChangeState(player.InteractionState);
            }

            else if (player.CheckIfCanAttack())
            {
                stateMachine.ChangeState(player.Attack1State);
            }
        }

        else if (inventoryInput)
        {
            player.InputHandler.UseInventoryInput();
            player.inventoryManager.ActivateInventory();
            stateMachine.ChangeState(player.InteractionState);
        }

        else if (dashInput)
        {
            player.InputHandler.UseDashInput();
            if (player.CheckIfCanDash())
            { 
                stateMachine.ChangeState(player.DashState); 
            }
        }

        else if (jumpInput)
        {
            player.InputHandler.UseJumpInput();
            stateMachine.ChangeState(player.JumpState);
        }

        else if (player.CheckIfInDialogue())
        {
            stateMachine.ChangeState(player.InteractionState);
        }

        else if (!isGrounded)
        {
            stateMachine.ChangeState(player.InAirState);
        }    
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
