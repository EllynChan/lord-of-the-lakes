using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingMinigameChase_Evasion : MonoBehaviour
{
    /// <summary>
    /// This script is used on the fish and controls it moving up and down
    /// the water to seem like it is evading the catchingbar.
    /// </summary>

    //The max and min heights we can go (Should be Water_Top and Water_Bottom objects)
    [SerializeField] private RectTransform maxHeight;
    [SerializeField] private RectTransform minHeight;

    [Range(0, 5f)] public float moveSpeed; //How fast the fish move
    public float maxWaitTime, minWaitTime; //How long the fish waits before moving again

    private Vector3 currentDestination; //Where the fish is moving towards
    private Vector3 startPosition; //Where the fish always starts at the beginning of the game

    private bool waiting = false; //Used when waiting for a new destination

    private void Start()
    {
        currentDestination = RandomDestination(); //Give the fish a random direction to go to
    }

    private void OnEnable()
    {
        startPosition = transform.position;
        currentDestination = RandomDestination();
        waiting = false;
    }

    private void OnDisable()
    {
        transform.position = startPosition;
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, currentDestination, moveSpeed * Time.deltaTime); //Lerp towards the fishes current destination

        if (Vector3.Distance(transform.position, currentDestination) <= 1f && !waiting)
        { //If we get close and arent already waiting then get a new destination
            StartCoroutine(Wait());
        }
    }

    private IEnumerator Wait()
    { //Used to let the fish wait a bit before getting a new destination
        waiting = true;
        yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));
        currentDestination = RandomDestination();
        waiting = false;
    }

    private Vector3 RandomDestination()
    {
        //Pick a random height to go to, between the top and bottom but they are offset using the height of the fish so it doesnt overlpa
        var maxUp = maxHeight.position.y;
        var maxDown = minHeight.position.y;;
        var newHeight = Random.Range(maxUp, maxDown);

        return new Vector3(transform.position.x, newHeight, transform.position.z);
    }
}
