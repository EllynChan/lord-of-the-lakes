using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFishState : PlayerState
{
    // just setting this bool to true for now to test the animation
    bool fishHooked = true;
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
        // finish showing off the fish that was just caught (the FishCaught boolean is still true so its still in the caught state)
        // Debug.Log(player.Animator.GetBool("FishCaught"));
        if (player.Animator.GetBool("FishCaught") && Time.time > (this.startTime + 1.5f))
        {
            stateMachine.ChangeState(player.BoatState);
        }
        // change to fishcatch sprite, starts the timer so that the catch sprite stays for 1 second
        // TODO: here all the fish data should be added to inventory, display fish sprite and basic info
        // TODO: the fishHooked condition should be linked to some kinf of fish data, and indicates that the player has a fish on the line
        if (Input.GetKeyDown(KeyCode.F))
        {
            player.Animator.SetBool("IsFishing", false);
            if (fishHooked)
            {
                Debug.Log("fish caught");
                player.Animator.SetBool("FishCaught", true);
                this.startTime = Time.time;
                
            } else
            {
                stateMachine.ChangeState(player.BoatState);
            }
            
        }

    }
}
