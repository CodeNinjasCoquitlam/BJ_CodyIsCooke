using UnityEngine;
using System;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "DialogueSO", menuName = "Scriptable Objects Demo/Dialogue SO")]

public class DialogueSO : ScriptableObject
{
    public string firstDialogue;
    public string testDialogue;
    public List<String> randomDialogues;
    public List<String> orderedDialogue;
}
