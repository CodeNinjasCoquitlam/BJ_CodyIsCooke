using UnityEngine;
using UnityEngine.UI;
using static DialogueSO;

public class DialougeManager : MonoBehaviour
{
    [Header("GameObjects")]
    public GameObject Portrait;
    public GameObject Name;
    public GameObject DialogueBOX;
    public GameObject Choice1;
    public GameObject Choice2;
    public GameObject Choice3;
    public GameObject PressSpaceToSkip;
    public GameObject PortraitAnchor;
    [Header("Texts")]
    public Text DialogueBoxTEXT;
    public Text Choice1Text;
    public Text Choice2Text;
    public Text Choice3Text;
    public Text NameText;
    [Header("idk")]
    public PortraitManager portratmanager;
    public bool DialogueActive = false;
    public int activeDialogueIndexIntThing;
    string NextDialogue;
    int CurrentSpeachIndex;
    int CurrentConversationIndex;
    DialogueSO ActiveDialogueSO;

    int NextDialogueIndex;
    void Start()
    {
        DialogueBOX.SetActive(false);
        portratmanager = this.gameObject.GetComponent<PortraitManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (DialogueActive)
            {
                CloseDialogue();
            }
        }

        if (Input.GetKeyDown(KeyCode.Keypad1)) 
        {

        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {

        }
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            //get the next dialogue based on TalkingThingDialogue Next speech index
            //set the next speach index based on the nextDialogue object

            LoadChoices(ActiveDialogueSO, "", 0, NextDialogueIndex);

            DialogueBoxTEXT.text = NextDialogue;
            NextDialogueIndex = ActiveDialogueSO.dialogues[CurrentConversationIndex].Speach[CurrentSpeachIndex].NextSpeachIndexInt;
        }
    }

    public void InitiateDialouge(DialogueSO dialogueSO,string conversation, string speach)
    {
        ActiveDialogueSO = dialogueSO;
        if (!DialogueActive)
        {
            DialogueActive = true;
            portratmanager.CreatePortrait(dialogueSO);
            DialogueBOX.SetActive(true);
            DialogueBoxTEXT.text = dialogueSO.GetTalkingThingDialogueByName(conversation, speach).Blabber;

            LoadChoices(dialogueSO, conversation, dialogueSO.GetTalkingThingDialogueByName(conversation, speach).ChoicesIndexInt, int.Parse(speach));
        }
    }
    public void CloseDialogue()
    {       
        DialogueActive = false;
        portratmanager.ClearPortrait();
        DialogueBOX.SetActive(false);
        DialogueBoxTEXT.text = "";
    }

    public void loadDialouge(int dialogueIndex, int SpeachIndex, DialogueSO dialogueSO)
    {
        DialogueBoxTEXT.text = dialogueSO.dialogues[dialogueIndex].Speach[SpeachIndex].Blabber;
        CurrentSpeachIndex = SpeachIndex;
    }

    public void LoadChoices(DialogueSO dialogueSO, string conversation, int choices, int speach)
    {
        if (dialogueSO.GetTalkingThingDialogueByName(conversation, speach.ToString()).ChoicesIndexInt > 0)
        {
            Choice1.SetActive(true);
            Choice2.SetActive(true);
            Choice3.SetActive(true);
            Choice1Text.text = dialogueSO.GetChoicessByName(conversation, choices.ToString()).ThreeOptionText[0];
            Choice2Text.text = dialogueSO.GetChoicessByName(conversation, choices.ToString()).ThreeOptionText[1];
            Choice3Text.text = dialogueSO.GetChoicessByName(conversation, choices.ToString()).ThreeOptionText[2];
        }
        else if (dialogueSO.GetTalkingThingDialogueByName(conversation, speach.ToString()).ChoicesIndexInt == 0)
        {
            Choice1.SetActive(false);
            Choice2.SetActive(false);
            Choice3.SetActive(false);
        }
        if (dialogueSO.GetTalkingThingDialogueByName(conversation, speach.ToString()).NextSpeachIndexInt > 0)
        {
            NextDialogue = dialogueSO.GetConversationByName(conversation).Speach[dialogueSO.GetTalkingThingDialogueByName(conversation, speach.ToString()).NextSpeachIndexInt].Blabber;
        }
    }
}
