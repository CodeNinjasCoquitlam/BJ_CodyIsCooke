using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [Header("UI Components")]
    public GameObject DialogueBOX;
    public Text DialogueBoxTEXT;
    public Text NameText;
    public GameObject PressSpaceToSkip;

    [Header("Choice UI")]
    public GameObject Choice1;
    public GameObject Choice2;
    public GameObject Choice3;
    public Text Choice1Text;
    public Text Choice2Text;
    public Text Choice3Text;
    public Button Choice1Button;
    public Button Choice2Button;
    public Button Choice3Button;

    [Header("Portrait Setup")]
    public GameObject PortraitAnchor;
    private PortraitManager portraitManager;

    [Header("State")]
    public bool DialogueActive = false;

    private DialogueSO activeDialogueSO;
    private DialogueSO.Conversation currentConversation;
    private DialogueSO.TalkingThingDialogue currentSpeech;
    private bool standardChoicesActive = false;

    void Start()
    {
        if (portraitManager == null)
            portraitManager = GetComponent<PortraitManager>();

        
        if (Choice1Button != null) Choice1Button.onClick.AddListener(() => SelectOption(0));
        if (Choice2Button != null) Choice2Button.onClick.AddListener(() => SelectOption(1));
        if (Choice3Button != null) Choice3Button.onClick.AddListener(() => SelectOption(2));

        CloseDialogue();
    }

    void Update()
    {
        if (!DialogueActive) return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseDialogue();
            return;
        }

        if (standardChoicesActive)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
            {
                SelectOption(0);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
            {
                SelectOption(1);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
            {
                SelectOption(2);
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                AdvanceDialogue();
            }
        }
    }


    public void InitiateDialogue(DialogueSO dialogueSO, string conversationName, string initialSpeechID)
    {
        if (dialogueSO == null) return;

        activeDialogueSO = dialogueSO;
        currentConversation = activeDialogueSO.GetConversationByName(conversationName);

        if (currentConversation == null)
        {
            Debug.LogError($"Conversation '{conversationName}' not found in {dialogueSO.name}");
            return;
        }

        if (portraitManager != null && !DialogueActive)
            portraitManager.CreatePortrait(dialogueSO);

        DialogueActive = true;
        DialogueBOX.SetActive(true);

        DisplaySpeech(initialSpeechID);
    }

    public void JumpToDialogue(string speechID)
    {
        JumpToDialogue(activeDialogueSO, currentConversation.conversationName, speechID);
    }

    public void JumpToDialogue(DialogueObjet objet)
    {

    }

    public void JumpToDialogue(DialogueSO newSO, string conversationName, string speechID)
    {
       
        if (newSO != null && newSO != activeDialogueSO)
        {
            activeDialogueSO = newSO;

            if (portraitManager != null)
            {
                portraitManager.ClearPortrait();
                portraitManager.CreatePortrait(activeDialogueSO);
            }
        }

        if (activeDialogueSO == null)
        {
            Debug.LogError("JumpToDialogue failed: No Active DialogueSO set!");
            return;
        }

        currentConversation = activeDialogueSO.GetConversationByName(conversationName);

        if (currentConversation == null)
        {
            Debug.LogError($"JumpToDialogue failed: Conversation '{conversationName}' not found!");
            return;
        }

        DialogueActive = true;
        DialogueBOX.SetActive(true);

        DisplaySpeech(speechID);
    }

    public void JumpToDialogueSameSO(string conversationName, string speechID)
    {
        JumpToDialogue(null, conversationName, speechID);
    }

    private void DisplaySpeech(string speechID)
    {
        currentSpeech = activeDialogueSO.GetTalkingThingDialogueByName(currentConversation.conversationName, speechID);
        //Debug.Log($"loading dialogue {currentSpeech} at ID {speechID}");
        if (currentSpeech == null)
        {
            CloseDialogue();
            return;
        }

        DialogueBoxTEXT.text = currentSpeech.Blabber;
        UpdateChoices(currentSpeech.ChoicesIndexInt);
    }

    private void AdvanceDialogue()
    {
        if (currentSpeech == null || currentSpeech.NextSpeachIndexInt <= 0)
        {
            UpdateChoices(currentSpeech.ChoicesIndexInt);
            return;
        }

        DisplaySpeech(currentSpeech.NextSpeachIndexInt.ToString());
    
    }

    private void UpdateChoices(int choicesID)
    {
        if (choicesID <= 0)
        {
            ToggleChoices(false);
            return;
        }

        DialogueSO.Choicess choicesData = activeDialogueSO.GetChoicessByName(currentConversation.conversationName, choicesID.ToString());

        if (choicesData == null || choicesData.ThreeOptionText == null)
        {
            ToggleChoices(false);
            return;
        }

        ToggleChoices(true);

        Choice1Text.text = choicesData.ThreeOptionText.Length > 0 ? "1: " + choicesData.ThreeOptionText[0] : "";
        Choice2Text.text = choicesData.ThreeOptionText.Length > 1 ? "2: " + choicesData.ThreeOptionText[1] : "";
        Choice3Text.text = choicesData.ThreeOptionText.Length > 2 ? "3: " + choicesData.ThreeOptionText[2] : "";
    }

    private void SelectOption(int optionIndex)
    {
        if (currentSpeech == null || !standardChoicesActive) return;

        DialogueSO.Choicess choicesData = activeDialogueSO.GetChoicessByName(currentConversation.conversationName, currentSpeech.ChoicesIndexInt.ToString());

        if (choicesData == null) return;

        // 1. Invoke custom UnityEvent first (for gameplay side-effects like items/gold)
        if (choicesData.ThreeOptionEvents != null && optionIndex < choicesData.ThreeOptionEvents.Length)
        {
            choicesData.ThreeOptionEvents[optionIndex]?.Invoke();
        }

        // 2. Handle Data-Driven Dialogue Jumps
        if (choicesData.ChoiceJumps != null && optionIndex < choicesData.ChoiceJumps.Length)
        {
            DialogueSO.DialogueJumpTarget jump = choicesData.ChoiceJumps[optionIndex];

            if (jump != null && !string.IsNullOrEmpty(jump.targetSpeechID))
            {
                // If targetConversationName is empty, keep current conversation name
                string targetConvo = string.IsNullOrEmpty(jump.targetConversationName)
                    ? currentConversation.conversationName
                    : jump.targetConversationName;

                JumpToDialogueSameSO(targetConvo, jump.targetSpeechID);
                return;
            }
        }

        // Fallback: If no target speech ID is set, close the dialogue
        CloseDialogue();
    }

    private void ToggleChoices(bool show)
    {
        standardChoicesActive = show;

        if (Choice1 != null) Choice1.SetActive(show);
        if (Choice2 != null) Choice2.SetActive(show);
        if (Choice3 != null) Choice3.SetActive(show);
        if (PressSpaceToSkip != null) PressSpaceToSkip.SetActive(!show);
    }

    public void CloseDialogue()
    {
        DialogueActive = false;
        standardChoicesActive = false;
        currentConversation = null;
        currentSpeech = null;

        if (portraitManager != null)
            portraitManager.ClearPortrait();

        if (DialogueBOX != null)
            DialogueBOX.SetActive(false);

        if (DialogueBoxTEXT != null)
            DialogueBoxTEXT.text = "";

        ToggleChoices(false);
    }
}

[System.Serializable]
public class DialogueObjet
{
    DialogueSO dso;
    string convoID;
    string speechId;
}