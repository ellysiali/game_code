using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newChargeStateData", menuName = "Data/State Data/Charge State")]

public class D_ChargeState : ScriptableObject
{
    public float movementSpeed = 6f, jumpVelocity = 10f;
    public float chargeDuration = 2f;
}
