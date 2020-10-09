using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    #region State Variables
    public CoinStateMachine StateMachine { get; private set; }
    public CoinFallState CoinFallState { get; private set; }
    public CoinSpinState CoinSpinState { get; private set; }
    public CoinIdleState CoinIdleState { get; private set; }
    #endregion

    #region Components
    public Rigidbody2D RB { get; private set; }
    public Animator Anim { get; private set; }
    [SerializeField] private CoinData coinData;
    [SerializeField] private PlayerData playerData;
    [SerializeField] private Transform groundCheck;

    #endregion

    #region Other Variables
    private float facingDirection;
    private Vector2 workspace;
    private Vector2 currentVelocity;
    #endregion

    #region Unity Callback Functions
    private void Awake()
    {
        StateMachine = new CoinStateMachine();
        CoinFallState = new CoinFallState(this, StateMachine, coinData, "Fall");
        CoinSpinState = new CoinSpinState(this, StateMachine, coinData, "Spin");
        CoinIdleState = new CoinIdleState(this, StateMachine, coinData, "Idle");
    }
    private void Start()
    {
        Anim = GetComponent<Animator>();
        RB = GetComponent<Rigidbody2D>();
        StateMachine.Initialize(CoinFallState);

        facingDirection = -1;
    }
    private void Update()
    {
        StateMachine.CurrentState.LogicUpdate();
    }
    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
        currentVelocity = RB.velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerData.coinCount += coinData.value;
            Destroy(gameObject);
        }
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
    public bool CheckIfGrounded => Physics2D.OverlapCircle(groundCheck.position, coinData.groundCheckRadius, coinData.PlatformLayerMask);
    public float CheckFacingDirection => facingDirection;
    public float CheckValue() => coinData.value;
    #endregion

    #region Other Functions
    public void Flip()
    {
        transform.Rotate(0f, 180f, 0f);
        facingDirection *= -1;
    }

    public virtual void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();

    public virtual void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();
    public virtual void OnDrawGizmos()
    {
        // GroundCheck
        //Gizmos.DrawWireSphere(groundCheck.position, coinData.groundCheckRadius);
    }
    #endregion
}
