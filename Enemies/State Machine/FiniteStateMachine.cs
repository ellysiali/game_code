using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;

public class FiniteStateMachine
{
    public State CurrentState { get; private set;}

    /**************************************************************************
    Function: 	 Initialize
    Description: when the class is initialized, perform given operations
    Parameters:  startingState - the state to start/initialize to
    *************************************************************************/
    public void Initialize(State startingState)
    {
        CurrentState = startingState;
        CurrentState.Enter();
    }

    /**************************************************************************
    Function: 	 changeState
    Description: change the state of the state machine to a different one 
    Parameters:  newState - the state to change to
    *************************************************************************/
    public void ChangeState(State newState)
    {
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }
}
