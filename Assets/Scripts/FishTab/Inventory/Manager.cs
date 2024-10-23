
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public GameObject[] tabs;
    public GameObject cellPrefab;     // Assign your cell prefab here
    public GameObject inventoryGrid;  // Assign your grid panel here
    private List<GameObject> inventorySlots = new List<GameObject>();
    public int maxInventorySlots = 30; // 30 cells in the grid
    protected Player player;

    // Start is called before the first frame update
    public void Start()
    {
        for (int i = 0; i < maxInventorySlots; i++)
        {
            GameObject newSlot = Instantiate(cellPrefab, inventoryGrid.transform);
            inventorySlots.Add(newSlot);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            Image slotImage = inventorySlots[i].GetComponent<Image>();
            if (slotImage.sprite == null) // Check for an empty slot
            {
                Fish fish = player.fishInventory[i];
                // TODO: figure out how to use fish.speciesId to get the image in ScriptableObject
                // slotImage.sprite =
                // fish.speciesId
                // slotImage.sprite = fishSprites[fishIndex]; // Add fish sprite
                break;
            }
        }
    }
}
