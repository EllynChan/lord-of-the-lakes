using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CGManager : MonoBehaviour
{
    public static CGManager Instance;

    [System.Serializable]
    public class CGEntry
    {
        public int key; 
        public Sprite sprite;
    }


    [SerializeField] List<CGEntry> cgList; // Assign sprites in the Inspector

    private Dictionary<int, Sprite> cgDictionary;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        // Initialize dictionary
        cgDictionary = new Dictionary<int, Sprite>();
        foreach (CGEntry entry in cgList)
        {
            cgDictionary[entry.key] = entry.sprite;
        }
    }

    public Sprite GetCG(int key)
    {
        if (cgDictionary.TryGetValue(key, out Sprite sprite))
        {
            return sprite;
        }
        Debug.LogError("CG not found for key: " + key);
        return null;
    }
}