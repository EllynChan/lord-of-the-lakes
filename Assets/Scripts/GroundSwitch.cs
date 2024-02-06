using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GroundSwitch : MonoBehaviour
{
    [SerializeField] private bool triggerActive = false;
    public TextMeshProUGUI prompt; 
    public Transform roadSpawn;
    public Transform lakeSpawn;
    private GameObject player; 
    private GameObject stateManagerObj;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            triggerActive = true;
            prompt.gameObject.SetActive(true);
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            triggerActive = false;
            prompt.gameObject.SetActive(false);
        }
    }
    
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        stateManagerObj = GameObject.FindGameObjectWithTag("StateManager");
    }

    private void Update()
    {
        if (triggerActive && Input.GetKeyDown(KeyCode.E)) 
        {
            RepositionPlayer();
        }
    }

    private void RepositionPlayer()
    {
        StateManager stateManager = stateManagerObj.GetComponent<StateManager>();
        if (stateManager.CurrentState == stateManager.WalkingState)
        {
            player.GetComponent<Transform>().position = lakeSpawn.position;
            player.GetComponent<Transform>().rotation = Quaternion.Euler(new Vector3(0,0,0));
            stateManager.SwitchState(stateManager.BoatingState);
        }
        else if (stateManager.CurrentState == stateManager.BoatingState)
        {
            player.GetComponent<Transform>().position = roadSpawn.position;
            player.GetComponent<Transform>().rotation = Quaternion.Euler(new Vector3(0,0,0));
            stateManager.SwitchState(stateManager.WalkingState);
        }
    }
}
