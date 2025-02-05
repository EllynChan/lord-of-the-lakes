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
        public int? texture_ally; // ally1=51, ally2=80
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
    [SerializeField] private List<Sprite> mcSprites;
    [SerializeField] private List<Sprite> ally1Sprites;
    [SerializeField] private List<Sprite> ally2Sprites;

    private Dictionary<string, DialogueEvent> dialogues;
    private Queue<ConversationLine> currentDialogueQueue;
    private DialogueEvent currentDialogueEvent;

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
        currentDialogueQueue = new Queue<ConversationLine>(); 
    }

    public void StartDialogue(string eventKey)
    {
        uiCanvas.SetActive(true);
        //Debug.Log(dialogues[eventKey].conversation["line_0"].text);
        currentDialogueEvent = dialogues[eventKey];
        for (int i = 0; i < currentDialogueEvent.num_lines; i++)
        {
            currentDialogueQueue.Enqueue(currentDialogueEvent.conversation["line_"+i]);
        }
        NextDialogue();
    }

    public void NextDialogue()
    {
        if (currentDialogueQueue.Count < 1)
        {
            EndDialogue();
            return;
        }

        ConversationLine cl = currentDialogueQueue.Dequeue();
        dialogueText.text = cl.text;
        nameText.text = cl.name;

        /*if (cl.frame_mc != null)
        {
            mcImage.sprite = mcSprites[ (int) cl.frame_mc];
        }
        else
        {
            mcImage = null;
        }

        if (cl.frame_ally != null)
        {
            if (currentDialogueEvent.texture_ally == 51)
            {
                allyImage.sprite = ally1Sprites[(int)cl.frame_ally];
            } 
            else if (currentDialogueEvent.texture_ally == 80)
            {
                allyImage.sprite = ally2Sprites[(int)cl.frame_ally];
            }
        } 
        else
        {
            allyImage = null;
        }*/

    }

    void EndDialogue()
    {
        uiCanvas.SetActive(false);
        // handle popup
    }
}
