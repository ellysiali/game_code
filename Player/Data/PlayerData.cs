using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Base Data")]
public class PlayerData : ScriptableObject
{
    [Header("Player Stats")]
    public float maxHealth = 100f;
    public float currentHealth = 100f;
    public float coinCount = 0f;
    public float attackMultiplier = 1f;
    public float defenseMultiplier = 1f;

    [Header("Transition Info")]
    public float startXPosition = 0f;
    public float startYPosition = -0.772f;
    public bool flipOnStart = false;

    [Header("Move State")]
    public float movementVelocity = 8f;

    [Header("Jump State")]
    public float jumpVelocity = 21f;

    [Header("In Air State")]
    public float variableJumpHeightMultiplier = 0.4f;
    public float variableJumpDelay = 0.2f;

    [Header("Dash State")]
    public float dashVelocity = 15f;
    public float dashDuration = 0.32f;

    [Header("Attack1 State")]
    public float attack1Damage = 10f;
    public float attack1VelocityMultiplier = 0f;
    public float attackTriggerDelay = 0.15f;
    public float attack1KnockbackX = 3f;
    public float attack1KnockbackY = 3f;

    [Header("Attack2 State")]
    public float attack2Damage = 10f;
    public float attack2VelocityMultiplier = 0f;
    public float attack2KnockbackX = 3f;
    public float attack2KnockbackY = 3f;

    [Header("Attack3 State")]
    public float attack3Damage = 20f;
    public float attack3VelocityMultiplier = 0f;
    public float attack3KnockbackX = 5f;
    public float attack3KnockbackY = 5f;

    [Header("Dead State")]
    public ParticleSystem deathParticle;

    [Header("Check Variables")]
    public float groundCheckRadius = 0.3f;
    public LayerMask PlatformLayerMask;
    public LayerMask EnemyLayerMask;
    public LayerMask NPCLayerMask;
    public float dashCooldown = 0.5f;
    public float attackCooldown = 0.15f;
    public float invulnerableDuration = 0.5f;
}
