using UnityEngine;

public class PortraitManager : MonoBehaviour
{
    private GameObject ObjectToClone;
    private GameObject PortraitAnchor;
    private GameObject clone;

    private Vector3 Offset;
    private Vector3 OffsetRot;
    private Vector3 OffsetScale;

    public DialougeManager dialoguemanager;
    

    void start()
    {
        this.gameObject.GetComponent<DialougeManager>();
    }

    public void CreatePortrait()
    {
        PortraitAnchor = dialoguemanager.PortraitAnchor;
        clone = Instantiate(ObjectToClone, PortraitAnchor.transform.position + Offset, Quaternion.Euler(OffsetRot));
    }

    public void ClearPortrait()
    {
        Destroy(ObjectToClone);
    }
}
