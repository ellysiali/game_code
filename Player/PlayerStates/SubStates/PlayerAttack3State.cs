using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack3State : PlayerAbilityState
{
    private AttackDetails attackDetails;
    public PlayerAttack3State(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName) : base(player, stateMachine, playerData, animationBoolName)
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
                                (player.comboAttackHitBox.position,
                                 player.comboAttackHitBox.GetComponent<CircleCollider2D>().radius,
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
        player.SetVelocityX(0f);

        attackDetails.damageAmount = playerData.attack3Damage * playerData.attackMultiplier;
        attackDetails.position = player.transform.position;
        attackDetails.knockbackX = playerData.attack3KnockbackX;
        attackDetails.knockbackY = playerData.attack3KnockbackY;
    }

    public override void Exit()
    {
        base.Exit();
        player.lastComboAttack = Time.time;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
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
            input.x *= playerData.attack3VelocityMultiplier;
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
