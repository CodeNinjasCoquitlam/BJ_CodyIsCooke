using UnityEditor.Build.Content;
using UnityEngine;

public class StartDialogue : MonoBehaviour
{
    public DialogueHolder dialogueHolder;
    public string jame;
    public string id;
    private DialogueManager gameymanagery;

    void Start()
    {
        gameymanagery = GameObject.Find("GameManager").GetComponent<DialogueManager>();
    }

    public void DoDialogue()
    {
        gameymanagery.InitiateDialogue(dialogueHolder.GetDialogueSO(), jame, id);
    }
}
