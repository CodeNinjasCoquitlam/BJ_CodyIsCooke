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

        // Wire UI buttons as alternative input method
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

        // Handle Choice Selection via Number Keys (Keypad & Top Row)
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
            // Advance dialogue on Space when no choices are active
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

        DialogueActive = true;
        DialogueBOX.SetActive(true);

        if (portraitManager != null)
            portraitManager.CreatePortrait(dialogueSO);

        DisplaySpeech(initialSpeechID);
    }

    /// <summary>
    /// Call this function inside UnityEvents (e.g. choice events) to jump to a specific 
    /// conversation and speech ID, with an optional new DialogueSO.
    /// </summary>
    public void JumpToDialogue(DialogueSO newSO, string conversationName, string speechID)
    {
        // If a new DialogueSO is assigned, switch to it and recreate the portrait
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

    /// <summary>
    /// Overload for JumpToDialogue when staying on the currently active DialogueSO.
    /// </summary>
    public void JumpToDialogueSameSO(string conversationName, string speechID)
    {
        JumpToDialogue(null, conversationName, speechID);
    }

    private void DisplaySpeech(string speechID)
    {
        currentSpeech = activeDialogueSO.GetTalkingThingDialogueByName(currentConversation.conversationName, speechID);

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
            CloseDialogue();
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

        // Store active speech before invoking event in case an event calls JumpToDialogue
        DialogueSO.TalkingThingDialogue cachedSpeech = currentSpeech;

        if (choicesData != null && choicesData.ThreeOptionEvents != null && optionIndex < choicesData.ThreeOptionEvents.Length)
        {
            choicesData.ThreeOptionEvents[optionIndex]?.Invoke();
        }

        // Only auto-advance if the choice event didn't trigger a JumpToDialogue call
        if (currentSpeech == cachedSpeech)
        {
            AdvanceDialogue();
        }
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