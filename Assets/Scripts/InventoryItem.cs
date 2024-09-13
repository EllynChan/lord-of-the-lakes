using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    [HideInInspector] public Item item;

    [Header("UI")]
    public Image image;

    private void Start()
    {
        InitializeItem(item);
    }

    // Start is called before the first frame update
    void InitializeItem(Item newItem)
    {
        item = newItem;
        image.sprite = newItem.image;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
