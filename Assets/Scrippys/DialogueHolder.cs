using UnityEngine;
using UnityEngine.Events;

public class DialogueHolder : MonoBehaviour
{
    public DialogueSO dialogueSO;
    public ChoicePair[] pairs;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public DialogueSO GetDialogueSO()
    {
        foreach (ChoicePair pair in pairs)
        {
            DialogueSO.Conversation convo = dialogueSO.dialogues[pair.convoIndex];
            DialogueSO.Choicess choices = convo.Choices[pair.choiceIndex];
            choices.ThreeOptionEvents[pair.optionIndex] = pair.choiceEvent;
        }
        return dialogueSO;
    }
}

[System.Serializable]
public class ChoicePair
{
    public int convoIndex;
    public int choiceIndex;
    public int optionIndex;
    public UnityEvent choiceEvent;
}
