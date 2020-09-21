using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newMeleeAttackStateData", menuName = "Data/State Data/Melee Attack State Data")]

public class D_MeleeAttackState : ScriptableObject
{
    public float attackRadius = 0.5f, damage = 10f;
    public LayerMask enemyLayerMask, playerLayerMask;
    public float friendlyKnockbackX = 0f;
    public float friendlyKnockbackY = 0f;
    public float knockbackX = 5f;
    public float knockbackY = 10f;
}
