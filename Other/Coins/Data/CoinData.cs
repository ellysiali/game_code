using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newCoinData", menuName = "Data/Coin Data/Base Data")]

public class CoinData : ScriptableObject
{
    [Header("Stats")]
    public float value = 1f;

    [Header("Fall State")]
    public float initialVelocityX = 2f;
    public float initialVelocityY = 5f;

    [Header("Spin State")]
    public float spinDuration = 1.5f;

    [Header("Idle State")]
    public float minTimeUntilFlip = 1f;
    public float maxTimeUntilFlip = 3f;
    public float minTimeUntilJump = 3f;
    public float maxTimeUntilJump = 5f;
    public float jumpX = 5f;
    public float jumpY = 2f;

    [Header("Check Variables")]
    public LayerMask PlatformLayerMask;
    public float groundCheckRadius = 0.2f;
}
