using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack2State : PlayerAbilityState
{
    private AttackDetails attackDetails;
    private bool attackInput;
    public PlayerAttack2State(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName) : base(player, stateMachine, playerData, animationBoolName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
        base.AnimationTrigger();
        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll
                                (player.basicAttackHitBox.position,
                                 player.basicAttackHitBox.GetComponent<CircleCollider2D>().radius,
                                 playerData.EnemyLayerMask);
        foreach (Collider2D collider in detectedObjects)
        {
            collider.transform.SendMessage("Damage", attackDetails);
        }
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        attackDetails.damageAmount = playerData.attack2Damage * playerData.attackMultiplier;
        attackDetails.position = player.transform.position;
        attackDetails.knockbackX = playerData.attack2KnockbackX;
        attackDetails.knockbackY = playerData.attack2KnockbackY; 
        attackInput = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        if (player.InputHandler.AttackInput)
        {
            if (Time.time - playerData.attackTriggerDelay >= startTime && player.CheckIfGrounded())
            {
                attackInput = player.InputHandler.AttackInput;
            }
            player.InputHandler.UseAttackInput();
        }
        if (isAbilityDone && attackInput)
        {
            player.StateMachine.ChangeState(player.Attack3State);
        }
        else
        {
            base.LogicUpdate();
        }
        if (isAnimationFinished)
        {
            isAbilityDone = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        Vector2 input = player.InputHandler.MovementInput;
        if (player.CheckIfGrounded())
        {
            input.x *= playerData.attack2VelocityMultiplier;
        }

        player.SetVelocityX(playerData.movementVelocity * input.x);

        if (Time.time - playerData.attackTriggerDelay >= startTime)
        {
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
}
