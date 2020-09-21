using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newFollowStateData", menuName = "Data/State Data/Follow State")]

public class D_FollowState : ScriptableObject
{
    public float movementSpeed = 7f, jumpVelocity = 14f;
    public float followDuration = 5f;
}
