using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Player : MonoBehaviour
{
    #region State Variables
    public PlayerStateMachine StateMachine { get; private set; }
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerInAirState InAirState { get; private set; }
    public PlayerFallState FallState { get; private set; }
    public PlayerDashState DashState { get; private set; }
    public PlayerAttack1State Attack1State { get; private set; }
    public PlayerAttack2State Attack2State { get; private set; }
    public PlayerAttack3State Attack3State { get; private set; }
    public PlayerHurtState HurtState { get; private set; }
    public PlayerDeadState DeadState { get; private set; }
    public PlayerInteractionState InteractionState { get; private set; }

    #endregion

    #region Components
    public PlayerInputHandler InputHandler { get; private set; }
    public Rigidbody2D RB { get; private set; }
    public Animator Anim { get; private set; }
    [SerializeField] private PlayerData playerData;
    public NPC_Controller NPC;
    public GameManager gameManager;
    public StoreManager storeManager;
    public InventoryManager inventoryManager;
    public CapsuleCollider2D capsuleCollider;
    public GameObject menu;

    #endregion

    #region Transforms
    [SerializeField] private Transform groundCheck;
    [SerializeField] public Transform basicAttackHitBox;
    [SerializeField] public Transform comboAttackHitBox;
    #endregion

    #region Other Variables
    public Vector2 currentVelocity { get; private set; }
    private Vector2 workspace;
    public Inventory inventory;
    public bool isInvulnerable, isInDialogueRange;
    public int facingDirection, lastDamageDirection;
    public float lastDash = -100f, lastComboAttack = -100f, lastTimeDamaged = -100f;
    #endregion

    #region Unity Callback Functions
    private void Awake()
    {
        StateMachine = new PlayerStateMachine();
        IdleState = new PlayerIdleState(this, StateMachine, playerData, "Idle");
        MoveState = new PlayerMoveState(this, StateMachine, playerData, "Move");
        JumpState = new PlayerJumpState(this, StateMachine, playerData, "Jump");
        InAirState = new PlayerInAirState(this, StateMachine, playerData, "Jump");
        FallState = new PlayerFallState(this, StateMachine, playerData, "Fall");
        DashState = new PlayerDashState(this, StateMachine, playerData, "Dash");
        Attack1State = new PlayerAttack1State(this, StateMachine, playerData, "Attack1");
        Attack2State = new PlayerAttack2State(this, StateMachine, playerData, "Attack2");
        Attack3State = new PlayerAttack3State(this, StateMachine, playerData, "Attack3");
        HurtState = new PlayerHurtState(this, StateMachine, playerData, "Hurt");
        DeadState = new PlayerDeadState(this, StateMachine, playerData, "Dead");
        InteractionState = new PlayerInteractionState(this, StateMachine, playerData, "Idle");
    }
    private void Start()
    {
        Anim = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        RB = GetComponent<Rigidbody2D>();
        StateMachine.Initialize(IdleState);
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        storeManager = GameObject.Find("StoreManager").GetComponent<StoreManager>();
        inventoryManager = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();

        isInvulnerable = false;
        facingDirection = 1;
        InputHandler.SetActionMapToGameplay();

        workspace.Set(playerData.startXPosition, playerData.startYPosition);
        RB.position = workspace;
        if (playerData.flipOnStart)
        {
            Flip();
        }
    }
    private void Update()
    {
        StateMachine.CurrentState.LogicUpdate();
        CheckIfChangeInvulnerability();
        currentVelocity = RB.velocity;
        CheckIfChangeDialogueRange();
    }
    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }
    #endregion

    #region Set Functions
    public void SetVelocityX(float velocity)
    {
        workspace.Set(velocity, currentVelocity.y);
        RB.velocity = workspace;
        currentVelocity = workspace;
    }

    public void SetVelocityY(float velocity)
    {
        workspace.Set(currentVelocity.x, velocity);
        RB.velocity = workspace;
        currentVelocity = workspace;
    }    
    #endregion 

    #region Check Functions
    public void CheckIfShouldFlip(int xInput)
    {
        if (xInput != 0 && xInput != facingDirection)
        {
            Flip();
        }
    }
    public bool CheckIfGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, playerData.groundCheckRadius, playerData.PlatformLayerMask);
    }
    public bool CheckIfCanDash() => Time.time >= lastDash + playerData.dashCooldown;
    public bool CheckIfCanAttack() => Time.time >= lastComboAttack + playerData.attackCooldown;
    public void CheckIfChangeInvulnerability()
    {
        if (isInvulnerable && Time.time >= lastTimeDamaged + playerData.invulnerableDuration)
        {
            isInvulnerable = false;
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        }

    }
    public bool CheckIfInDialogue()
    {
        if (NPC != null)
        {
            return NPC.CheckIfDialogueActive();
        }
        else
        {
            return false;
        }
    }
    public bool CheckIfChangeDialogueRange()
    {
        isInDialogueRange = Physics2D.OverlapCapsule(capsuleCollider.transform.position, capsuleCollider.size, CapsuleDirection2D.Vertical, 0f, playerData.NPCLayerMask);
        if (isInDialogueRange)
        {
            NPC = Physics2D.OverlapCapsule(capsuleCollider.transform.position, capsuleCollider.size, CapsuleDirection2D.Vertical, 0f, playerData.NPCLayerMask).gameObject.GetComponent<NPC_Controller>();
        }
        else
        {
            NPC = null;
        }
        return isInDialogueRange;
    }
    public bool CheckIfInStore() => storeManager.CheckIfStoreActive();
    public bool CheckIfInInventory() => inventoryManager.CheckIfInventoryActive();
    public bool CheckIfFullHealth() => playerData.currentHealth == playerData.maxHealth;
    #endregion

    #region Other Functions
    public void Flip()
    {
        facingDirection *= -1;
        transform.Rotate(0f, 180f, 0f);
    }
    public virtual void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();
    public virtual void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();
    public virtual void Damage(AttackDetails attackDetails)
    {
        if (!isInvulnerable)
        {
            playerData.currentHealth -= attackDetails.damageAmount * playerData.defenseMultiplier;
            lastDamageDirection = attackDetails.position.x > transform.position.x ? -1 : 1;
            if (lastDamageDirection == facingDirection)
            {
                Flip();
            }

            SetVelocityX(-facingDirection * attackDetails.knockbackX);
            SetVelocityY(-facingDirection * attackDetails.knockbackY);

            if (playerData.currentHealth <= 0)
            {
                StateMachine.ChangeState(DeadState);
            }
            else
            {
                StateMachine.ChangeState(HurtState);
                lastTimeDamaged = Time.time;
                isInvulnerable = true;
                GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, .75f);
            }
        }
    }
    public virtual void ResetHealth() => playerData.currentHealth = playerData.maxHealth;
    public virtual void AddHealth(float value)
    {
        if (playerData.currentHealth + value <= playerData.maxHealth)
        {
            playerData.currentHealth += value;
        }
        else
        {
            playerData.currentHealth = playerData.maxHealth;
        }
    }
    public virtual void OnDrawGizmos()
    {
    }
    #endregion
}
