using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static DialogueManager;

public class DialogueManager : MonoBehaviour
{
    [System.Serializable]
    public class DialogueEvent
    {
        public Dictionary<string, ConversationLine> conversation;
        public PopUp popup;
        public int num_lines;
        public int? texture_ally;
    }

    [System.Serializable]
    public class ConversationLine
    {
        public string name;
        public string text;
        public int? frame_mc;
        public int? frame_ally;
        public int? background;
    }
    public class PopUp
    {
        public string text;
        public int? image;
    }

    public static DialogueManager Instance;

    [SerializeField] private GameObject uiCanvas;
    [SerializeField] private TextMeshProUGUI nameText; 
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private Image mcImage;
    [SerializeField] private Image allyImage;

    [SerializeField] private TextAsset dialogueJson; // Reference to JSON
    private Dictionary<string, DialogueEvent> dialogues;
    private Queue<ConversationLine> currentDialogueQueue;

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
        
        dialogues = JsonUtility.FromJson<Dictionary<string, DialogueEvent>>(dialogueJson.text);
        Debug.Log(dialogueJson.text);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
