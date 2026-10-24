using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueHolder : MonoBehaviour
{
    [Header("1. Scriptable Object")]
    public DialogueSO dialogueSO;

    [Header("Event Injections")]
    public List<ChoiceEventPair> eventPairs;

    public DialogueSO GetDialogueSO()
    {
        if (dialogueSO == null) return null;

        // Inject scene events into the ScriptableObject safely by matching IDs
        foreach (ChoiceEventPair pair in eventPairs)
        {
            foreach (DialogueSO.Conversation convo in dialogueSO.dialogues)
            {
                // Match the Conversation ID
                if (convo.conversationName == pair.ConversationID)
                {
                    foreach (DialogueSO.Choicess choice in convo.Choices)
                    {
                        // Match the Choice Set ID
                        if (choice.choicesID == pair.choicesID)
                        {
                            if (choice.ThreeOptionEvents == null || choice.ThreeOptionEvents.Length <= pair.optionIndex)
                            {
                                System.Array.Resize(ref choice.ThreeOptionEvents, pair.optionIndex + 1);
                            }
                            // Inject the scene function into the specific choice option (0, 1, or 2)
                            choice.ThreeOptionEvents[pair.optionIndex] = pair.choiceEvent;
                        }
                    }
                }
            }
        }
        return dialogueSO;
    }
}

[System.Serializable]
public class ChoiceEventPair
{
    [Header("2. Convo ID (e.g., 'Inital')")]
    public string ConversationID;

    [Header("3. Choice Set ID (e.g., '1' or 'ShopChoices')")]
    public string choicesID;

    [Header("4. Which choice in the set of 3 (0, 1, or 2)")]
    public int optionIndex;

    [Header("5. Function to run in the scene")]
    public UnityEvent choiceEvent;
}