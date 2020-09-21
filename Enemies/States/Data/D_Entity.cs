using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newEntityData", menuName = "Data/Entity Data/Base Data")]
public class D_Entity : ScriptableObject
{
    [Header("Stats")]
    public float maxHealth = 50;
    public float touchDamage = 10f;
    public float knockbackX = 5f;
    public float knockbackY = 10f;

    [Header("Check Variables")]
    public float wallCheckDistance = 0.2f;
    public float ledgeCheckDistance = 0.4f;
    public float minAgroDistance = 3f;
    public float maxAgroDistance = 4f;
    public float closeRangeActionDistance = 1f;
    public float groundCheckRadius = 0.3f;
    public float touchDamageWidth = 2f;
    public float touchDamageHeight = 2f;
    public float heightUntilJump = 1f;
    public float maxPlayerRange = 6f;
    public float minPlayerRange = 1.5f;

    [Header("LayerMasks & GameObjects")]
    public LayerMask platformLayerMask;
    public LayerMask enemyLayerMask;
    public LayerMask playerLayerMask;
    public GameObject hitParticle;
}
