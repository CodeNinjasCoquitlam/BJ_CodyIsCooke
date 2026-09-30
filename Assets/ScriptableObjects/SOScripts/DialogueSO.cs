using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static UnityEngine.Splines.SplineInstantiate;

[CreateAssetMenu(fileName = "DialogueSO", menuName = "Scriptable Objects Dialogue/Dialogue SO")]

public class DialogueSO : ScriptableObject
{
    [Header("Portrait Settings")]
    public GameObject ThingTalking;
    public Vector3 TalkingThingOffset;
    public Quaternion TalkingThingOffsetRotation;
    public Vector3 TalkingThingScaleOffsetType;


    [Header("Dialogues")]
    public Conversation[] dialogues;
    public List<String> RandomFIRSTDialogues;

    public Conversation GetConversationByName(string name)
    {
        foreach (Conversation conversation in dialogues)
        {
            if (conversation.conversationName == name)
            {
                return conversation;
            }
        }
        return null;
    }

    public TalkingThingDialogue GetTalkingThingDialogueByName(string conversationName, string speechID)
    {
        Conversation conv = GetConversationByName(conversationName);
        if (conv == null) return null;

        foreach (TalkingThingDialogue speech in conv.Speach)
        {
            if (speech.ThisSpeachIndex == speechID)
                return speech;
        }
        return null;
    }

    public Choicess GetChoicessByName(string conversationName, string choicesID)
    {
        Conversation conv = GetConversationByName(conversationName);
        if (conv == null) return null;

        foreach (Choicess choice in conv.Choices)
        {
            if (choice.choicesID == choicesID)
                return choice;
        }
        return null;
    }

    [System.Serializable]
    public class Conversation
    {
        [Header("Name Of This Individual Array")]
        public string conversationName;
        [Header("What The thing talking is saying")]
        public TalkingThingDialogue[] Speach;
        [Header("what the player can say back")]
        public Choicess[] Choices;
    }

    [System.Serializable]
    public class Choicess
    {
        [Header("Id Of this Set Of choices")]
        public string choicesID;
        [Header("what the player can say or do as options (MAKE SURE TO ADD (Say Hello) or (Walk Away)))")]
        public string[] ThreeOptionText;
        [Header("Functions from unity that u can run")]
        public UnityEvent[] ThreeOptionEvents;
    }

    [System.Serializable]
    public class TalkingThingDialogue
    {
        public string ThisSpeachIndex;
        public string Blabber;
        public int ChoicesIndexInt;
        public int NextSpeachIndexInt;
    }
}