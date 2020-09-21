using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newJumpStateData", menuName = "Data/State Data/Jump State")]

public class D_JumpState : ScriptableObject
{
    public float jumpVelocity = 10f;
    public float movementSpeed = 4f;
}
