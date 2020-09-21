using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newMoveStateData", menuName = "Data/State Data/Move State")]

public class D_MoveState : ScriptableObject
{
    public float movementSpeed = 2, minMoveTime = 2f, maxMoveTime = 4f;
}
