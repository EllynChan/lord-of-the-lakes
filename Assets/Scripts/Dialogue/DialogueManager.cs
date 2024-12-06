using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;

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
        dialogues = JsonConvert.DeserializeObject<Dictionary<string, DialogueEvent>>(dialogueJson.text);

        /*foreach (string k in dialogues.Keys)
        {
            Debug.Log(k);
        }*/
    }

    void StartDialogue(string eventKey)
    {
        uiCanvas.SetActive(true);
    }

    void EndDialogue()
    {
        uiCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
