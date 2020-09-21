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
    public CapsuleCollider2D capsuleCollider;
    #endregion

    #region Other Variables
    public int facingDirection { get; private set; }
    private Vector2 velocityWorkspace;
    private int lastDamageDirection;
    private float currentHealth;
    private AttackDetails attackDetails;
    protected bool isStunned, isDead;
    public bool isFriendly;
    protected Collider2D colliderResults; 
    #endregion

    #region Unity Callback Functions
    /**************************************************************************
    Function: 	 Start
    Description: Initializes the necessary variables before the first update
    *************************************************************************/
    public virtual void Start()
    {
        facingDirection = 1;
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        atsm = GetComponent<AnimationToStateMachine>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        stateMachine = new FiniteStateMachine();

        currentHealth = entityData.maxHealth;
        healthIcon.gameObject.SetActive(false);
    }

    /**************************************************************************
    Function: 	 Update
    Description: Once per frame, implements the following code
    *************************************************************************/
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

    /**************************************************************************
    Function: 	 FixUpdate
    Description: 
    *************************************************************************/
    public virtual void FixedUpdate()
    {
        stateMachine.CurrentState.PhysicsUpdate();
    }
    #endregion

    #region Set Functions
    /**************************************************************************
    Function: 	 SetVelocityX
    Description: Sets the x velocity (movement speed) of the entity
    *************************************************************************/
    public virtual void SetVelocityX(float velocity)
    {
        velocityWorkspace.Set(facingDirection * velocity, rigidBody.velocity.y);
        rigidBody.velocity = velocityWorkspace;
    }

    /**************************************************************************
    Function: 	 SetVelocityY
    Description: Sets the y velocity (jump speed) of the entity
    *************************************************************************/
    public virtual void SetVelocityY(float velocity)
    {
        velocityWorkspace.Set(rigidBody.velocity.x, velocity);
        rigidBody.velocity = velocityWorkspace;
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
    public virtual bool CheckEnemyInMinAgroRange()
    {
        if (isFriendly)
        {
            colliderResults = Physics2D.OverlapCircle(transform.position, entityData.minAgroDistance, entityData.enemyLayerMask);
            if (colliderResults)
            {
                return Physics2D.OverlapCircle(transform.position, entityData.minAgroDistance, entityData.enemyLayerMask);
            }
            else
            {
                return false;
            }
        }
        else
        {
            return Physics2D.OverlapCircle(transform.position, entityData.minAgroDistance, entityData.playerLayerMask);

        }
    }
    public virtual bool CheckEnemyInMaxAgroRange()
    {
        if (isFriendly)
        {
            colliderResults = Physics2D.OverlapCircle(transform.position, entityData.maxAgroDistance, entityData.enemyLayerMask);
            if (colliderResults)
            {
                return Physics2D.OverlapCircle(transform.position, entityData.maxAgroDistance, entityData.enemyLayerMask);
            }
            else
            {
                return false;
            }
        }
        else
        {
            return Physics2D.OverlapCircle(transform.position, entityData.maxAgroDistance, entityData.playerLayerMask);
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
            colliderResults = Physics2D.OverlapCircle(transform.position, entityData.maxAgroDistance, entityData.enemyLayerMask);
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
        Collider2D hit = Physics2D.OverlapCapsule(capsuleCollider.transform.position, capsuleCollider.size, capsuleCollider.direction, 0f, entityData.playerLayerMask);

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
        //// GroundCheck
        //Gizmos.DrawLine(ledgeCheck.position,
        //                ledgeCheck.position + (Vector3)(Vector2.down * entityData.ledgeCheckDistance));

        //// WallCheck
        //Gizmos.DrawLine(wallCheck.position,
        //                wallCheck.position + (Vector3)(Vector2.right * facingDirection * entityData.wallCheckDistance));

        //AgroCheck
        //Gizmos.DrawWireSphere(transform.position, entityData.minAgroDistance);

        //Gizmos.DrawLine(topLeft, topRight);
        //Gizmos.DrawLine(topLeft, botLeft);
        //Gizmos.DrawLine(botLeft, botRight);
        //Gizmos.DrawLine(botRight, topRight);
    }
    #endregion
}
