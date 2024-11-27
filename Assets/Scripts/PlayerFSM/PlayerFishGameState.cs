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
    private GameObject fishIcon;
    private GameObject catchingBar;

    [Header("Settings")]
    [SerializeField] private float catchMultiplier = 10f; //Higher means catch fish faster x
    [SerializeField] private float catchingForce = 30000; //How much force to push the catchingbar up by

    private Fish currentFishOnLine;
    private bool beingCaught; 
    public bool reelingFish = false;

    //This is the fish on the UI that you are chasing to catch
    private GameObject fishBar;
    private FishingMinigameChase_Collision fishChaseCollision; //Reference to this script on the fish
    private bool inTrigger = false; //Whether or not the fish is inside the "catchingbar"

    private float catchPercentage = 0f; //0-100 how much you have caught the fish
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

        fishMinigameChase = "/Player/PlayerCanvas/FishMinigame_Chase";
        fishMinigameMash = "/Player/PlayerCanvas/FishMinigame_Mash";
        fishMinigameRhythm = "/Player/PlayerCanvas/FishMinigame_Rhythm";
        fishMinigameHold = "/Player/PlayerCanvas/FishMinigame_HoldRelease";

    }

    public override void Exit()
    {
        player.Animator.SetBool("FishCaught", false);
        player.Animator.SetBool("IsFishing", false);
        reelingFish = false;
        player.fishCaughtPanel.SetActive(false);
        player.fishCaughtPanel.transform.position = fishCaughtPanelLeftPos;
        base.Exit();
    }

    public override void Update()
    {
        //string fishMinigameString = fishMinigameChase; // TODO: later must make it depend on a condition to change the types of minigames
        fishMinigameCanvas = GameObject.Find(fishMinigameChase);
        fishIcon = GameObject.Find(fishMinigameChase + "/WaterBar/FishIcon");
        catchingBar = GameObject.Find(fishMinigameChase + "/WaterBar/CatchingBar");

        catchProgressBar = GameObject.Find(fishMinigameChase + "/CatchProgressBar").GetComponent<UnityEngine.UI.Slider>(); //The bar on the right that shows how much you have caught

        catchingBarRB = catchingBar.GetComponent<Rigidbody2D>(); //Get reference to the Rigidbody on the catchingbar
        fishMinigameCanvas.SetActive(true);

        if (Input.GetMouseButton(0))
        {
            Debug.Log("click");
            catchingBarRB.AddForce(Vector2.up * catchingForce * Time.deltaTime, ForceMode2D.Force); //Add force to lift the bar
        }

        //If the fish is in our trigger box
        if (beingCaught && player.Animator.GetCurrentAnimatorStateInfo(0).IsName("Fishing"))
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
            catchPercentage = 0;
            FishCaught();
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
