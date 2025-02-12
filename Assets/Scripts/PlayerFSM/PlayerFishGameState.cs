using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PlayerFishGameState : PlayerState
{
    //types of minigames
    private string fishMinigameChase;
    private string fishMinigameMash;
    private string fishMinigameRhythm;
    private string fishMinigameHold;

    private Rigidbody2D catchingBarRB;
    private GameObject fishMinigameCanvas;
    private GameObject catchingBar;

    private float catchMultiplier = 30f; //Higher means catch fish faster x
    private float catchingForce = 1000; //How much force to push the catchingbar up by

    private Fish currentFishOnLine;
    public bool reelingFish = false;

    //This is the fish on the UI that you are chasing to catch
    private GameObject fishBar;
    private FishingMinigameChase_Collision fishChaseCollision; //Reference to this script on the fish
    private bool inTrigger = false; //Whether or not the fish is inside the "catchingbar"

    private float catchPercentage = 20f; //0-100 how much you have caught the fish
    private UnityEngine.UI.Slider catchProgressBar; //The bar on the right that shows how much you have caught


    Vector3 fishCaughtPanelLeftPos;

    public PlayerFishGameState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        fishCaughtPanelLeftPos = player.fishCaughtPanel.transform.position;
        player.Animator.SetBool("IsFishMinigame", true);
        reelingFish = true;
        catchPercentage = 40f;

        fishMinigameChase = "/Player/PlayerCanvas/FishMinigame_Chase";
        fishMinigameMash = "/Player/PlayerCanvas/FishMinigame_Mash";
        fishMinigameRhythm = "/Player/PlayerCanvas/FishMinigame_Rhythm";
        fishMinigameHold = "/Player/PlayerCanvas/FishMinigame_HoldRelease";

        //string fishMinigameString = fishMinigameChase; // TODO: later must make it depend on a condition to change the types of minigames
        fishMinigameCanvas = GameObject.Find(fishMinigameChase);
        catchingBar = GameObject.Find(fishMinigameChase + "/WaterBar/CatchingBar");

        catchProgressBar = GameObject.Find(fishMinigameChase + "/CatchProgressBar").GetComponent<UnityEngine.UI.Slider>(); //The bar on the right that shows how much you have caught

        catchingBarRB = catchingBar.GetComponent<Rigidbody2D>(); //Get reference to the Rigidbody on the catchingbar
        fishMinigameCanvas.SetActive(true);
    }

    public override void Exit()
    {
        player.Animator.SetBool("FishCaught", false);
        player.Animator.SetBool("IsFishing", false);
        reelingFish = false;
        currentFishOnLine = null;
        player.fishCaughtPanel.SetActive(false);
        player.fishCaughtPanel.transform.position = fishCaughtPanelLeftPos;
        base.Exit();
    }

    public override void Update()
    {
        if (Input.GetMouseButton(0) || Input.GetKey(KeyCode.F))
        {
            catchingBarRB.AddForce(Vector2.up * catchingForce * Time.deltaTime, ForceMode2D.Force); //Add force to lift the bar
        }

        //If the fish is in our trigger box
        if (inTrigger && player.Animator.GetCurrentAnimatorStateInfo(0).IsName("Reeling"))
        {
            catchPercentage += catchMultiplier * Time.deltaTime;
        }
        else
        {
            catchPercentage -= catchMultiplier * Time.deltaTime;
        }

        //Clamps our percentage between 0 and 100
        catchPercentage = Mathf.Clamp(catchPercentage, 0, 100);
        catchProgressBar.value = catchPercentage;
        if (catchPercentage >= 100)
        { //Fish is caught if percentage is full
            FishCaught();
        }

        if (catchPercentage <= 0)
        {
            player.Animator.SetBool("IsFishMinigame", false);
            fishMinigameCanvas.SetActive(false); //Disable the fishing canvas
            stateMachine.ChangeState(player.BoatState);
        }

        
        if (player.Animator.GetCurrentAnimatorStateInfo(0).IsName("Catch"))
        {
            string playerSprite = player.GetComponent<SpriteRenderer>().sprite.name;
            if (playerSprite.Contains("right"))
            {
                player.fishCaughtPanel.transform.position = new Vector3(fishCaughtPanelLeftPos.x + 0.8f, fishCaughtPanelLeftPos.y, 0);
            }
            else if (playerSprite.Contains("up"))
            {
                player.fishCaughtPanel.transform.position = new Vector3(fishCaughtPanelLeftPos.x + 0.3f, fishCaughtPanelLeftPos.y + 0.4f, 0);
            }
            else if (playerSprite.Contains("down"))
            {
                player.fishCaughtPanel.transform.position = new Vector3(fishCaughtPanelLeftPos.x + 0.3f, fishCaughtPanelLeftPos.y, 0);
            }
            player.fishCaughtPanel.SetActive(true);
        }
        // show off the fish that was just caught (the FishCaught boolean is still true so its still in the caught state)
        // Debug.Log(player.Animator.GetBool("FishCaught"));
        if (player.Animator.GetBool("FishCaught") && Time.time >= (this.startTime + 1.5f))
        {
            player.Animator.SetBool("FishCaught", false);
            player.fishCaughtPanel.SetActive(false);
            stateMachine.ChangeState(player.BoatState);
        }

    }

    //Called when the catchpercentage hits 100
    public void FishCaught()
    {
        if (currentFishOnLine == null)
        { //This picks a new fish if the old one is lost by chance
            currentFishOnLine = FishManager.GetRandomFish(Rarity.common).Item1;
        }
        var tempSprite = Resources.Load<Sprite>($"FishSprites/{currentFishOnLine.speciesId}");
        player.fishCaughtImage.GetComponent<UnityEngine.UI.Image>().sprite = tempSprite;
        player.fishCaughtNameText.GetComponent<TMP_Text>().text = currentFishOnLine.name;

        Debug.Log(currentFishOnLine.name);
        player.Animator.SetBool("IsFishMinigame", false);
        player.Animator.SetBool("FishCaught", true);
        fishMinigameCanvas.SetActive(false); //Disable the fishing canvas
        this.startTime = Time.time;
    }

    //Called from the FishingMinigame_FishTrigger script
    public void FishInBar()
    {
        inTrigger = true;
    }

    //Called from the FishingMinigame_FishTrigger script
    public void FishOutOfBar()
    {
        inTrigger = false;
    }
}
