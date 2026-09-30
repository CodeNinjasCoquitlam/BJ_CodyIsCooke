using UnityEngine;

public class PortraitManager : MonoBehaviour
{
    private GameObject clone;

    private DialogueManager dialoguemanager;
    

    void Start()
    {
        dialoguemanager = this.gameObject.GetComponent<DialogueManager>();
        ClearPortrait();
    }

    public void CreatePortrait(DialogueSO dialogue)
    {
        clone = Instantiate(
            dialogue.ThingTalking,
            dialoguemanager.PortraitAnchor.transform.position + dialogue.TalkingThingOffset,
            dialogue.TalkingThingOffsetRotation
            );
    }

    public void ClearPortrait()
    {
        Destroy(clone);
    }
}
