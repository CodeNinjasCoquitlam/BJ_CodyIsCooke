using UnityEngine;

public class PortraitManager : MonoBehaviour
{
    private GameObject clone;

    public DialougeManager dialoguemanager;
    

    void Start()
    {
        dialoguemanager = this.gameObject.GetComponent<DialougeManager>();
        ClearPortrait();
    }

    public void CreatePortrait(DialogueSO dialogue)
    {
        
        clone = Instantiate(dialogue.ThingTalking, dialoguemanager.PortraitAnchor.transform.position + dialogue.TalkingThingOffset, dialogue.TalkingThingOffsetRotation);
    }

    public void ClearPortrait()
    {
        Destroy(clone);
    }
}
