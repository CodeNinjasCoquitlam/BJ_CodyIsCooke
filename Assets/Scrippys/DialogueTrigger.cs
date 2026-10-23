using UnityEngine;

[RequireComponent(typeof(DialogueHolder))]
public class DialogueTrigger : MonoBehaviour
{
    [Header("Dialogue Configuration")]
    public string conversationName;
    public string initialSpeechID = "1";

    private DialogueHolder dialogueHolder;
    private DialogueManager dialogueManager;

    void Awake()
    {
        dialogueHolder = GetComponent<DialogueHolder>();
    }

    void Start()
    {
        GameObject gm = GameObject.Find("GameManager");
        if (gm != null)
        {
            dialogueManager = gm.GetComponent<DialogueManager>();
        }
    }

    /// <summary>
    /// Parameterless method — perfect for UnityEvent (Interact.onInteract)!
    /// </summary>
    public void TriggerDialogue()
    {
        if (dialogueManager == null)
        {
            Debug.LogError("DialogueManager not found in scene on GameManager!");
            return;
        }

        if (dialogueHolder == null)
        {
            Debug.LogError($"DialogueHolder missing on {gameObject.name}!");
            return;
        }

        // 1. Get the SO with bound scene events
        DialogueSO so = dialogueHolder.GetDialogueSO();

        // 2. Start dialogue using configured parameters
        dialogueManager.InitiateDialogue(so, conversationName, initialSpeechID);
    }
}