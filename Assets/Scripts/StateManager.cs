using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    BaseState currState;
    public WalkingState WalkingState = new WalkingState();
    public BoatingState BoatingState = new BoatingState();
    public FishingState FishingState = new FishingState();
    public StaticState StaticState = new StaticState(); // all states where you can't control player

    void Start()
    {
        currState = WalkingState;
        currState.EnterState(this);
    }
 
    void Update()
    {
        currState.UpdateState(this);
    }

    public void SwitchState(BaseState state)
    {
        currState.ExitState(this);
        currState = state;
        state.EnterState(this);
    }

    public BaseState CurrentState
    {
        get { return currState; }
    }
}
