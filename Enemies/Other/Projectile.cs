using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private CircleCollider2D circleCollider;
    private AttackDetails attackDetails;
    Vector2 projectileSpeed;
    string enemyTag;
    LayerMask enemyLayerMask;
    private Rigidbody2D rigidBody;
    private float startTime;

    #region Unity Callback Functions
    public void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        rigidBody = GetComponent<Rigidbody2D>();
        startTime = Time.time;
    }
    public void Initialize(AttackDetails attackDetails, float speed, LayerMask enemyLayerMask, bool isFriendly)
    {
        this.attackDetails = attackDetails;
        projectileSpeed.Set(speed, 0f);
        this.enemyLayerMask = enemyLayerMask;
        if (isFriendly)
        {
            enemyTag = "Enemy";
        }
        else
        {
            enemyTag = "Player";
        }
    }
    public void Update()
    {
        rigidBody.velocity = projectileSpeed;
        if (Time.time >= startTime + 5f)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(enemyTag))
        {
            ApplyDamage();
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Platform"))
        {
            Destroy(gameObject);
        }
    }
    #endregion

    private void ApplyDamage()
    {
        Collider2D hit = Physics2D.OverlapCircle(circleCollider.transform.position, circleCollider.radius, enemyLayerMask);

        if (hit != null)
        {
            hit.SendMessage("Damage", attackDetails);
        }
    }
}
