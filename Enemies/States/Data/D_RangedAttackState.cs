using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newRangedAttackStateData", menuName = "Data/State Data/Ranged Attack State Data")]

public class D_RangedAttackState : ScriptableObject
{
    public float projectileSpeed = 2.5f, damage = 10f;
    public LayerMask enemyLayerMask, playerLayerMask;
    public float friendlyKnockbackX = 0f;
    public float friendlyKnockbackY = 0f;
    public float knockbackX = 5f;
    public float knockbackY = 10f;
    public GameObject projectile;
}
