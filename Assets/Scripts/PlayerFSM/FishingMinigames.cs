using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishingMinigames : MonoBehaviour
{ 
    [Header("Setup References")]
        //The catching bar is the green bar that you put ontop of the fish to catch it
    [SerializeField] private GameObject catchingbar;
    private Vector3 catchingBarLoc;
    private Rigidbody2D catchingBarRB;

    //This is the fish on the UI that you are chasing to catch
    [SerializeField] private GameObject fishBar;
    private FishingMinigameChase_Collision fishChaseCollision; //Reference to this script on the fish
    private bool inTrigger = false; //Whether or not the fish is inside the "catchingbar"

    private float catchPercentage = 0f; //0-100 how much you have caught the fish
    [SerializeField] private Slider catchProgressBar; //The bar on the right that shows how much you have caught

    [Header("Settings")]
    [SerializeField] private KeyCode fishingKey = KeyCode.Space; //Key used to play
    [SerializeField] private float catchMultiplier = 10f; //Higher means catch fish faster x
    [SerializeField] private float catchingForce; //How much force to push the catchingbar up by

    //types of minigames
    private string fishMinigameChase;
    private string fishMinigameMash;
    private string fishMinigameRhythm;
    private string fishMinigameHold;

    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        fishMinigameChase = "/Player/PlayerCanvas/FishMinigame_Chase";
        fishMinigameMash = "/Player/PlayerCanvas/FishMinigame_Mash";
        fishMinigameRhythm = "/Player/PlayerCanvas/FishMinigame_Rhythm";
        fishMinigameHold = "/Player/PlayerCanvas/FishMinigame_HoldRelease";

        catchProgressBar = GameObject.Find(fishMinigameChase + "/CatchProgressBar").GetComponent<UnityEngine.UI.Slider>(); //The bar on the right that shows how much you have caught

    }

    // Update is called once per frame
    void Update()
    {
        //string fishMinigameString = fishMinigameChase; // TODO: later must make it depend on a condition to change the types of minigames
        //fishMinigameCanvas = GameObject.Find(fishMinigameChase);
        //fishIcon = GameObject.Find(fishMinigameChase + "/WaterBar/FishIcon");
        //catchingBar = GameObject.Find(fishMinigameChase + "/WaterBar/CatchingBar");

        //catchProgressBar = GameObject.Find(fishMinigameChase + "/CatchProgressBar").GetComponent<UnityEngine.UI.Slider>(); //The bar on the right that shows how much you have caught

        //catchingBarRB = catchingBar.GetComponent<Rigidbody2D>(); //Get reference to the Rigidbody on the catchingbar
        //fishMinigameCanvas.SetActive(true);

        //if (Input.GetMouseButtonDown(0))
        //{
        //    catchingBarRB.AddForce(Vector2.up * catchingForce * Time.deltaTime, ForceMode2D.Force); //Add force to lift the bar
        //}
    }

    //Called when the catchpercentage hits 100
    public void FishCaught()
    {
        //if (currentFishOnLine == null)
        //{ //This picks a new fish if the old one is lost by chance
        //    currentFishOnLine = FishManager.GetRandomFish(Rarity.common).Item1;
        //}
        //var tempSprite = Resources.Load<Sprite>($"FishSprites/{currentFishOnLine.speciesId}");
        //player.fishCaughtImage.GetComponent<UnityEngine.UI.Image>().sprite = tempSprite;
        //player.fishCaughtNameText.GetComponent<TMP_Text>().text = currentFishOnLine.name;

        //Debug.Log(currentFishOnLine.name);
        //player.Animator.SetBool("IsFishMinigame", false);
        //player.Animator.SetBool("FishCaught", true);
        //fishMinigameCanvas.SetActive(false); //Disable the fishing canvas

    }

    public void SetPlayer(Player player) {  this.player = player; }

}
