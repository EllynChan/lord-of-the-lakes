using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingMinigameChase_Collision : MonoBehaviour
{
    /// <summary>
    /// We use this to detect whether or not the fish is in the catching bar
    /// and sends that info to the FishingMinigame.cs script.
    /// </summary>

    public bool beingCaught = false;
    [SerializeField] private Player player;
    private PlayerFishGameState minigameController;

    private void Start()
    {
        minigameController = player.FishGameState;
        if (player == null)
        {
            Debug.LogError("player is null");
        }
        Debug.Log("minigameController: " + minigameController);
        if (minigameController == null)
        {
            Debug.LogError("minigameController is null");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("OnTriggerEnter2D" + minigameController);
        if (minigameController.reelingFish)
        {
            if (other.CompareTag("CatchingBar") && !beingCaught)
            {
                beingCaught = true;
                minigameController.FishInBar();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("OnTriggerExit2D");
        if (other.CompareTag("CatchingBar") && beingCaught)
        {
            beingCaught = false;
            minigameController.FishOutOfBar();
        }
    }
}
