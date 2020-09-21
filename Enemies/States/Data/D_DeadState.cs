using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newDeadStateData", menuName = "Data/State Data/Dead State")]

public class D_DeadState : ScriptableObject
{
    public ParticleSystem deathParticle;
    public float deadDuration = 0.1f;
    public float averageCoinDrops = 2;
    public float coinRange = 1;
    public GameObject copper, silver, gold;
}
