using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected Player player;
    protected PlayerStateMachine stateMachine;

    protected float startTime; // reset time every time state switches so we can keep track of state duration

    public PlayerState(Player player, PlayerStateMachine stateMachine)
    {
        this.player = player;
        this.stateMachine = stateMachine;
    }
    

    public virtual void Enter()
    {
        startTime = Time.time;
    }

    public virtual void Exit()
    {
        
    }

    public virtual void Update()
    {

    }


}
