using System;
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

    public Item[] inventoryItems; // Assign your InventoryItems in the Inspector

    public Item GetItemById(string id)
    {
        foreach (Item item in inventoryItems)
        {
            if (item.type == ItemType.Fish && item.id == id)
                return item;
        }
        return null; // Return null if no item matches
    }

    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    // Start is called before the first frame update
    public void Start()
    {
        for (int i = 0; i < maxInventorySlots; i++)
        {
            GameObject newSlot = Instantiate(cellPrefab, inventoryGrid.transform);
            inventorySlots.Add(newSlot);
        }

        RefreshInventoryUI();
    }

    public void RefreshInventoryUI()
    {
        List<Fish> fishInventory = player.fishInventory;
        Debug.Log("player.fishInventory.Count: " + fishInventory.Count);
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            Image slotImage = inventorySlots[i].GetComponent<Image>();
            if (i < fishInventory.Count)
            {
                Fish fish = fishInventory[i];
                Item inventoryItem = GetItemById(fish.speciesId);
                Debug.Log("player.fishInventory fish.speciesId: " + fish.speciesId);

                if (inventoryItem != null)
                {
                    Transform overlayTrans = inventorySlots[i].transform.Find("ItemImage");
                    if (overlayTrans != null)
                    {
                        Image overlayImage = overlayTrans.GetComponent<Image>();
                        overlayImage.sprite = inventoryItem.image;
                    }
                    else
                    {
                        Debug.Log("overlayTrans is null");
                    }
                }
            }
        }
    }
}
