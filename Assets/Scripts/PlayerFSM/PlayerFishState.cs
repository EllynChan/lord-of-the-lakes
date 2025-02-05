using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class PlayerFishState : PlayerState
{

    // just setting this bool to true for now to test the animation
    bool nibble = false;
    float nibbleTimer; // timer it takes to get a fish nibble
    float nibbleWaitTime; // Set time it takes to get a fish nibble calculated when entering this state; nibbleTimer ticks up until it reaches this time
    float catchTimer; // timer for how long player can wait to reach and catch the fish
    readonly float maxWaitTime = 6f; // can change this if we want it to be longer
    readonly float shinySpotMaxWaitTime = 3f;

    Vector3 exclamationLeftPos;

    public PlayerFishState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        nibble = false;
        Debug.Log("fishing now");
        player.Animator.SetBool("IsFishing", true);
        nibbleTimer = Time.time;

        float playerMaxWaitTime = player.isOnShinySpot ? shinySpotMaxWaitTime : maxWaitTime;
        nibbleWaitTime = Time.time + Random.Range(playerMaxWaitTime * 0.25f, playerMaxWaitTime);

        catchTimer = Time.time;
        exclamationLeftPos = player.exclamationMark.transform.position;

    }

    public override void Exit()
    {
        player.Animator.SetBool("FishCaught", false);
        player.Animator.SetBool("IsFishing", false);
        player.fishCaughtPanel.SetActive(false);
        player.exclamationMark.transform.position = exclamationLeftPos; // reset to original position, left is default
        base.Exit();
    }

    public override void Update()
    {
        // TODO: bug somethign is wrong with the catch timer (sometimes it triggers without nibble happening first)
        // Timer set ups
        nibbleTimer += Time.deltaTime; // timer for waiting when a fish will be on the line
        // indicates a fish is on the line, exclamation mark shows up
        if (nibbleTimer >= nibbleWaitTime && !nibble)
        {
            nibble = true;
            player.exclamationMark.SetActive(true);
            this.startTime = Time.time;
            catchTimer = Time.time;
            string playerSprite = player.GetComponent<SpriteRenderer>().sprite.name;
            Debug.Log(playerSprite);
            if (playerSprite.Contains("right"))
            {
                player.exclamationMark.transform.position = new Vector3(exclamationLeftPos.x + 0.8f, exclamationLeftPos.y, 0);
            }
            else if (playerSprite.Contains("up"))
            {
                player.exclamationMark.transform.position = new Vector3(exclamationLeftPos.x + 0.3f, exclamationLeftPos.y + 0.4f, 0);
            }
            else if (playerSprite.Contains("down"))
            {
                player.exclamationMark.transform.position = new Vector3(exclamationLeftPos.x + 0.3f, exclamationLeftPos.y, 0);
            }
        }
        if (nibble)
        {
            catchTimer += Time.deltaTime; // timer starts when nibble happens, if player is too slow the fish gets away
        }
        
        // if player takes too long to catch fish on exclamation mark, the fish gets away
        if (nibble & catchTimer > this.startTime + 2f)
        {
            nibble = false;
            player.Animator.SetBool("IsFishing", false);
            player.Animator.SetBool("FishCaught", false);
            player.exclamationMark.SetActive(false);
            stateMachine.ChangeState(player.BoatState);
        }
        // player presses F in time and should catch the fish that is on the line or cancel the fishing if there is no fish on the line
        if (Input.GetKeyDown(KeyCode.F))
        {
            player.Animator.SetBool("IsFishing", false);

            player.exclamationMark.SetActive(false);
            // there is a fish on the line, transition to a fishing minigame
            if (nibble)
            {
                stateMachine.ChangeState(player.FishGameState);
                
            } else // there is no fish on the line, return to movement state
            {
                stateMachine.ChangeState(player.BoatState);
            }
        }

    }




}
