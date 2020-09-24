using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    #region Components
    [SerializeField] private Transform player, wallCheck, ledgeCheck,
                                       groundCheck, healthIcon;
    public FiniteStateMachine stateMachine;
    public D_Entity entityData;
    public Rigidbody2D rigidBody { get; private set; }
    public Animator animator { get; private set; }
    public AnimationToStateMachine atsm { get; private set; }
    public BoxCollider2D boxCollider;
    #endregion

    #region Other Variables
    public int facingDirection { get; private set; }
    private Vector2 velocityWorkspace;
    private int lastDamageDirection;
    private float currentHealth, lastAttack = -100f;
    private AttackDetails attackDetails;
    protected bool isStunned, isDead;
    public bool isFriendly;
    protected Collider2D colliderResults; 
    #endregion

    #region Unity Callback Functions
    public virtual void Start()
    {
        facingDirection = 1;
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        atsm = GetComponent<AnimationToStateMachine>();
        boxCollider = GetComponent<BoxCollider2D>();
        stateMachine = new FiniteStateMachine();

        if(isFriendly)
        {
            SetFriendly();
        }

        currentHealth = entityData.maxHealth;
        healthIcon.gameObject.SetActive(false);
    }
    public virtual void Update()
    {
        stateMachine.CurrentState.LogicUpdate();
        healthIcon.gameObject.GetComponent<SpriteRenderer>().color =
            Color.HSVToRGB(currentHealth / entityData.maxHealth * 100 / 360, 1, 1);

        if (!isFriendly)
        {
            ApplyTouchDamage();
        }
    }
    public virtual void FixedUpdate()
    {
        stateMachine.CurrentState.PhysicsUpdate();
    }
    #endregion

    #region Set Functions
    public virtual void SetVelocityX(float velocity)
    {
        velocityWorkspace.Set(facingDirection * velocity, rigidBody.velocity.y);
        rigidBody.velocity = velocityWorkspace;
    }
    public virtual void SetVelocityY(float velocity)
    {
        velocityWorkspace.Set(rigidBody.velocity.x, velocity);
        rigidBody.velocity = velocityWorkspace;
    }
    public virtual void SetLastAttack() => lastAttack = Time.time;
    public virtual void SetFriendly()
    {
        isFriendly = true;
        gameObject.tag = "Summon";
        gameObject.layer = LayerMask.NameToLayer("Friendly");
    }
    #endregion

    #region Check Functions
    public virtual bool CheckWall()
    {
        return Physics2D.Raycast(wallCheck.position, transform.right,
                                 entityData.wallCheckDistance, 
                                 entityData.platformLayerMask);
    }
    public virtual bool CheckLedge()
    {
        return Physics2D.Raycast(ledgeCheck.position, Vector2.down,
                         entityData.ledgeCheckDistance,
                         entityData.platformLayerMask);
    }
    public virtual bool CheckEnemyInMinAggroRange()
    {
        if (isFriendly)
        {
            colliderResults = Physics2D.OverlapCircle(transform.position, entityData.minAggroDistance, entityData.enemyLayerMask);
            if (colliderResults)
            {
                return Physics2D.OverlapCircle(transform.position, entityData.minAggroDistance, entityData.enemyLayerMask);
            }
            else
            {
                return false;
            }
        }
        else
        {
            return Physics2D.OverlapCircle(transform.position, entityData.minAggroDistance, entityData.playerLayerMask);

        }
    }
    public virtual bool CheckEnemyInMaxAggroRange()
    {
        if (isFriendly)
        {
            colliderResults = Physics2D.OverlapCircle(transform.position, entityData.maxAggroDistance, entityData.enemyLayerMask);
            if (colliderResults)
            {
                return Physics2D.OverlapCircle(transform.position, entityData.maxAggroDistance, entityData.enemyLayerMask);
            }
            else
            {
                return false;
            }
        }
        else
        {
            return Physics2D.OverlapCircle(transform.position, entityData.maxAggroDistance, entityData.playerLayerMask);
        }
    }
    public virtual bool CheckEnemyInCloseRangeAction()
    {
        if (isFriendly)
        {
            colliderResults = Physics2D.OverlapCircle(transform.position, entityData.closeRangeActionDistance, entityData.enemyLayerMask);
            if (colliderResults)
            {
                return Physics2D.OverlapCircle(transform.position, entityData.closeRangeActionDistance, entityData.enemyLayerMask);
            }
            else
            {
                return false;
            }
        }
        else
        {
            return Physics2D.OverlapCircle(transform.position, entityData.closeRangeActionDistance, entityData.playerLayerMask);
        }
    }
    public virtual bool CheckGround()
    {
        return Physics2D.OverlapCircle(groundCheck.position, entityData.groundCheckRadius,
                                       entityData.platformLayerMask);
    }
    public virtual bool CheckFacingEnemy()
    {
        if (isFriendly)
        {
            colliderResults = Physics2D.OverlapCircle(transform.position, entityData.maxAggroDistance, entityData.enemyLayerMask);
            return colliderResults.transform.position.x > transform.position.x && facingDirection == 1 || colliderResults.transform.position.x < transform.position.x && facingDirection == -1;
        }
        else
        {
            return CheckFacingPlayer();
        }
    }
    public virtual bool CheckPlayerInMaxRange()
    {
        return Physics2D.Raycast(transform.position, (player.position - transform.position).normalized,
                                  entityData.maxPlayerRange, entityData.playerLayerMask);
    }
    public virtual bool CheckPlayerInMinRange()
    {
        return Physics2D.Raycast(transform.position, (player.position - transform.position).normalized,
                                  entityData.minPlayerRange, entityData.playerLayerMask);
    }
    public virtual bool CheckCanAttack() => Time.time >= lastAttack + entityData.attackCooldown;
    public virtual bool CheckFacingPlayer() => player.transform.position.x > transform.position.x && facingDirection == 1 || player.transform.position.x < transform.position.x && facingDirection == -1;
    public virtual Vector2 CheckPlayerPosition() => player.position;
    #endregion

    #region Other Functions
    public virtual void Damage (AttackDetails attackDetails)
    {
        currentHealth -= attackDetails.damageAmount;
        isStunned = true;

        if (!healthIcon.gameObject.activeSelf)
        {
            healthIcon.gameObject.SetActive(true);
        }

        if (attackDetails.position.x > transform.position.x)
        {
            lastDamageDirection = -1;
        }
        else
        {
            lastDamageDirection = 1;
        }

        if (lastDamageDirection == facingDirection)
        {
            Flip();
        }

        Instantiate(entityData.hitParticle,
            new Vector2(rigidBody.transform.position.x,
                         rigidBody.position.y),
            Quaternion.Euler(0f, 0f, Random.Range(0f, 135f) * lastDamageDirection));
        
        DamageKnockBack(attackDetails.knockbackX, attackDetails.knockbackY);

        if(currentHealth <= 0)
        {
            isDead = true;
        }
    }
    public virtual void DamageKnockBack(float horizontalVelocity, float verticalVelocity)
    {
        velocityWorkspace.Set(horizontalVelocity * lastDamageDirection, verticalVelocity);
        rigidBody.velocity = velocityWorkspace;
    }
    public virtual void Flip()
    {
        facingDirection *= -1;
        transform.Rotate(0f, 180f, 0f);
    }
    public virtual void ApplyTouchDamage()
    {
        Collider2D hit = Physics2D.OverlapBox(boxCollider.transform.position, boxCollider.size, 0f, entityData.playerLayerMask);

        if (hit != null)
        {
            attackDetails.damageAmount = entityData.touchDamage;
            attackDetails.position = transform.position;
            attackDetails.knockbackX = entityData.knockbackX;
            attackDetails.knockbackY = entityData.knockbackY;
            hit.SendMessage("Damage", attackDetails);
        }
    }

    public virtual void OnDrawGizmos()
    {
        //// WallCheck
        //Gizmos.DrawLine(wallCheck.position,
        //                wallCheck.position + (Vector3)(Vector2.right * facingDirection * entityData.wallCheckDistance));

        //// LedgeCheck
        //Gizmos.DrawLine(wallCheck.position,
        //                wallCheck.position + (Vector3)(Vector2.down * facingDirection * entityData.wallCheckDistance));

        //// GroundCheck
        //Gizmos.DrawWireSphere(groundCheck.position, entityData.groundCheckRadius);
    }
    #endregion
}
