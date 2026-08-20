using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Events;

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
}

[System.Serializable]
public class Conversation
{
    public string conversationName;
    public string[] person1Dialogue;
    public Choicess[] Choices;
}

[System.Serializable]
public class Choicess
{
    public int choicesID;
    public string[] ThreeOptionText;
    public UnityEvent[] ThreeOptionEvents;
}