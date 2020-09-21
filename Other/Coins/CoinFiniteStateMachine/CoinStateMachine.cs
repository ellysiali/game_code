using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinStateMachine
{
    public CoinState CurrentState { get; private set; }

    public void Initialize(CoinState startingState)
    {
        CurrentState = startingState;
        CurrentState.Enter();
    }

    public void ChangeState(CoinState newState)
    {
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }
}
