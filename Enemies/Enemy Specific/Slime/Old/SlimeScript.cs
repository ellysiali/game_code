//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class SlimeScript : MonoBehaviour
//{
//    [SerializeField] private LayerMask platformLayerMask, playerLayerMask;
//    [SerializeField] private Transform groundCheck, wallCheck, touchDamageCheck;
//    [SerializeField] private GameObject hitParticle;
//    [SerializeField] private ParticleSystem deathParticle;
//    [SerializeField] private float maxHealth, basicKBSpeed, basicKBElevation,
//                                   comboKBSpeed, comboKBElevation, movementSpeed,
//                                   touchDamage, touchDamageWidth, touchDamageHeight;

//    private PlayerScript playerScript;
//    private Rigidbody2D rigidBody;
//    private Animator mobAnimation;
//    private AnimatorStateInfo animationState;
//    private Transform triangle;

//    private bool groundDetected, wallDetected;
//    private int facingDirection = 1;
//    private float currentHealth, groundCheckDistance = 0.5f, wallCheckdistance = 0.1f;
//    private float[] attackDetails = new float[2];
//    private Vector2 touchDamageBottomLeft, touchDamageTopRight;


//    /**************************************************************************
//     Function: 	  Start
//     Description: Initializes the necessary variables before the first update
//     *************************************************************************/
//    void Start()
//    {
//        triangle = transform.GetChild(0);
//        triangle.gameObject.SetActive(false);
//        currentHealth = maxHealth;
//        playerScript = GameObject.Find("Player").GetComponent<PlayerScript>();
//        mobAnimation = GetComponent<Animator>();
//        rigidBody = GetComponent<Rigidbody2D>();
//    }

//    /**************************************************************************
//    Function: 	 Update
//    Description: Once per frame, implements the following code
//    *************************************************************************/
//    void Update()
//    {
//        // Change color of triangle to health respectively
//        triangle.gameObject.GetComponent<SpriteRenderer>().color =
//            Color.HSVToRGB(currentHealth / maxHealth * 100 / 360, 1, 1);

//        animationState = mobAnimation.GetCurrentAnimatorStateInfo(0);
//        if (Animator.StringToHash("Move") == animationState.shortNameHash)
//        {
//            UpdateMovement();
//        }

//        CheckTouchDamage();
//    }

//    /**************************************************************************
//    Function: 	 updateMovement
//    Description: Update the movement, changing directions as needed
//    *************************************************************************/
//    private void UpdateMovement()
//    {
//        groundDetected = Physics2D.Raycast(groundCheck.position, Vector2.down,
//                                           groundCheckDistance, platformLayerMask);
//        wallDetected = Physics2D.Raycast(wallCheck.position, transform.right,
//                                           wallCheckdistance, platformLayerMask);

//        if (!groundDetected || wallDetected)
//        {
//            Flip();
//        }
//        else
//        {
//            rigidBody.velocity = new Vector2(facingDirection * movementSpeed,
//                                              rigidBody.velocity.y);
//        }
//    }

//    /**************************************************************************
//    Function: 	 flip
//    Description: Flip the enemy
//    *************************************************************************/
//    private void Flip()
//    {
//        facingDirection *= -1;
//        if (facingDirection == 1)
//        {
//            transform.localScale = new Vector2(1, 1);
//        }
//        else
//        {
//            transform.localScale = new Vector2(-1, 1);
//        }
//    }

//    /**************************************************************************
//    Function: 	 Damage
//    Description: Applies damage to enemy and adjusts corresponding properties
//    Parameters:  attackDetails - the details of the attack, expressed as an array;
//                                 Contains details on:
//                                   [0] - the damage amount
//                                   [1] - the x-position of the attacker
//    *************************************************************************/
//    private void Damage(float[] attackDetails)
//    {
//        int attackerDirection;
//        // Activate the triangle icon and adjust health
//        triangle.gameObject.SetActive(true);
//        currentHealth -= attackDetails[0];

//        // Face attacker
//        if (attackDetails[1] > transform.position.x)
//        {
//            transform.localScale = new Vector2(1, 1);
//            attackerDirection = -1;
//        }
//        else
//        {
//            transform.localScale = new Vector2(-1, 1);
//            attackerDirection = 1;
//        }

//        // Change animation and add hit particle
//        mobAnimation.SetTrigger("Damage");
//        Instantiate(hitParticle, 
//                    new Vector2 (rigidBody.transform.position.x, 
//                                 rigidBody.position.y), 
//                    Quaternion.Euler(0f, 0f, Random.Range(0f, 135f) * attackerDirection));

//        if (currentHealth > 0f)
//        {           
//            // Add knockback if not dead
//            if (attackDetails[0] == playerScript.comboAttackDamage)
//            {
//                rigidBody.velocity = new Vector2(attackerDirection * comboKBSpeed,
//                                                 comboKBElevation);
//            }
//            else
//            {
//                rigidBody.velocity = new Vector2(attackerDirection * basicKBSpeed,
//                                                 basicKBElevation);
//            }
//        }
//        else
//        {
//            // Otherwise, trigger death animation
//            mobAnimation.SetTrigger("Death");
//        }
//    }

//    /**************************************************************************
//    Function: 	 Die
//    Description: Kill enemy; is activated within an animation function
//    *************************************************************************/
//    private void Die()
//    {
//        deathParticle.transform.position = rigidBody.transform.position;
//        Instantiate(deathParticle);
//        Destroy(gameObject);
//    }

//    /**************************************************************************
//    Function: 	 checkTouchDamage
//    Description: Check for touch damage and send damage if so
//    *************************************************************************/
//    private void CheckTouchDamage()
//    {
//        touchDamageBottomLeft.Set(touchDamageCheck.position.x - (touchDamageWidth / 2),
//                                  touchDamageCheck.position.y - (touchDamageHeight / 2));
//        touchDamageTopRight.Set(touchDamageCheck.position.x + (touchDamageWidth / 2),
//                                touchDamageCheck.position.y + (touchDamageHeight / 2));
//        Collider2D hit = Physics2D.OverlapArea(touchDamageBottomLeft, touchDamageTopRight, playerLayerMask);
        
//        if (hit != null)
//        {
//            attackDetails[0] = touchDamage;
//            attackDetails[1] = transform.position.x;
//            hit.SendMessage("Damage", attackDetails);
//        }
//    }

//    /**************************************************************************
//    Function: 	 OnDrawGizmos
//    Description: Debug function; used to visualize properties when playing
//    *************************************************************************/
//    private void OnDrawGizmos()
//    {
//        //// GroundCheck
//        //Gizmos.DrawLine(groundCheck.position,
//        //                new Vector2(groundCheck.position.x,
//        //                            groundCheck.position.y - groundCheckDistance));

//        //// WallCheck
//        //Gizmos.DrawLine(wallCheck.position,
//        //                new Vector2(wallCheck.position.x + wallCheckdistance,
//        //                            wallCheck.position.y));

//        // TouchDamageCheck
//        Vector2 botLeft = new Vector2(touchDamageCheck.position.x - (touchDamageWidth / 2), touchDamageCheck.position.y - (touchDamageHeight / 2));
//        Vector2 botRight = new Vector2(touchDamageCheck.position.x + (touchDamageWidth / 2), touchDamageCheck.position.y - (touchDamageHeight / 2));
//        Vector2 topLeft = new Vector2(touchDamageCheck.position.x - (touchDamageWidth / 2), touchDamageCheck.position.y + (touchDamageHeight / 2));
//        Vector2 topRight = new Vector2(touchDamageCheck.position.x + (touchDamageWidth / 2), touchDamageCheck.position.y + (touchDamageHeight / 2));

//        Gizmos.DrawLine(topLeft, topRight);
//        Gizmos.DrawLine(topLeft, botLeft);
//        Gizmos.DrawLine(botLeft, botRight);
//        Gizmos.DrawLine(botRight, topRight);
//    }
//}
