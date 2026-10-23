using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueHolder : MonoBehaviour
{
    public DialogueSO dialogueSO;
    public List<ChoiceEventPair> eventPairs;

    public DialogueSO GetDialogueSO()
    {
        if (dialogueSO == null) return null;

        // Inject scene events into the ScriptableObject safely by matching string IDs
        foreach (ChoiceEventPair pair in eventPairs)
        {
            foreach (DialogueSO.Conversation convo in dialogueSO.dialogues)
            {
                foreach (DialogueSO.Choicess choice in convo.Choices)
                {
                    if (choice.choicesID == pair.choicesID)
                    {
                        if (choice.ThreeOptionEvents == null || choice.ThreeOptionEvents.Length <= pair.optionIndex)
                        {
                            System.Array.Resize(ref choice.ThreeOptionEvents, pair.optionIndex + 1);
                        }
                        choice.ThreeOptionEvents[pair.optionIndex] = pair.choiceEvent;
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
    [Header("Match Choice By ID (e.g., '1' or 'ShopChoices')")]
    public string choicesID;
    [Header("Option 0, 1, or 2")]
    public int optionIndex;
    [Header("Function to run in the scene")]
    public UnityEvent choiceEvent;
}