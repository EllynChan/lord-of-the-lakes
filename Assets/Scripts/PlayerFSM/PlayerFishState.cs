using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFishState : PlayerState
{
    public PlayerFishState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine)
    {
    }

    public override void Enter()
    {
        
        base.Enter();
        Debug.Log("fishing now");
        player.Animator.SetBool("IsFishing", true);
    }

    public override void Exit()
    {
        player.Animator.SetBool("FishCaught", false);
        player.Animator.SetBool("IsFishing", false);
        base.Exit();
    }

    public override void Update()
    {

        if (Input.GetKeyDown(KeyCode.F))
        {
            stateMachine.ChangeState(player.BoatState);
        }
    }
}
