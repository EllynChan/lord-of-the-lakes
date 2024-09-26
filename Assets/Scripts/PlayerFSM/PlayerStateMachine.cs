using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine 
{ 

    public PlayerState _currentState {  get; private set; }

    public void Initialize(PlayerState startingState)
    {
        _currentState = startingState;
        _currentState.Enter();
    }

    public void ChangeState(PlayerState newState)
    {
        _currentState.Exit();
        _currentState = newState;
        _currentState.Enter();
    }
}
