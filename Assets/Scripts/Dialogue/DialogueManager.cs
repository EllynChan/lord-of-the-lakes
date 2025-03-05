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
    [SerializeField] private GameObject canvasMain;
    [SerializeField] private GameObject canvasPopUp;
    [SerializeField] private TextMeshProUGUI nameText; 
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private GameObject mcImage;
    [SerializeField] private GameObject allyImage;
    [SerializeField] private GameObject cg;

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
            // if popup, handle popup, else end dialogue
            if (currentDialogueEvent.popup != null)
            {
                handlePopUp();
            } 
            else
            {
                EndDialogue();
            }
            return;
        }

        ConversationLine cl = currentDialogueQueue.Dequeue();
        dialogueText.text = cl.text;
        nameText.text = cl.name;

        // handle sprite changes
        if (cl.frame_mc != null)
        {
            mcImage.SetActive(true);
            mcImage.GetComponent<Image>().sprite = mcSprites[(int)cl.frame_mc];
            if (cl.name == "Jonah" || cl.name == "???")
            {
                mcImage.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            } else
            {
                mcImage.GetComponent<Image>().color = new Color32(150, 150, 150, 255);
            }
        }
        else
        {
            mcImage.SetActive(false);
        }

        if (cl.frame_ally != null)
        { 
            allyImage.SetActive(true);
            if (currentDialogueEvent.texture_ally == 51)
            {
                allyImage.GetComponent<Image>().sprite = ally1Sprites[(int)cl.frame_ally];
                if (cl.name == "Nix" || cl.name == "Strange Girl")
                {
                    allyImage.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                }
                else
                {
                    allyImage.GetComponent<Image>().color = new Color32(150, 150, 150, 255);
                }
            }
            else if (currentDialogueEvent.texture_ally == 80)
            {
                allyImage.GetComponent<Image>().sprite = ally2Sprites[(int)cl.frame_ally];
                if (cl.name == "Lance")
                {
                    allyImage.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                }
                else
                {
                    allyImage.GetComponent<Image>().color = new Color32(150, 150, 150, 255);
                }
            }
        }
        else
        {
            allyImage.SetActive(false);
        }

        if (cl.background != null)
        {
            cg.SetActive(true);
            cg.GetComponent<Image>().sprite = CGManager.Instance.GetCG((int)cl.background);
        }
        else
        {
            cg.SetActive(false);
        }

    }

    public void EndDialogue()
    {
        uiCanvas.SetActive(false);
    }

    void handlePopUp()
    {
        // todo
    }
}
