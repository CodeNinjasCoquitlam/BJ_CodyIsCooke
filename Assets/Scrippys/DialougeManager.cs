using UnityEngine;
using UnityEngine.UI;

public class DialougeManager : MonoBehaviour
{
    [Header("GameObjects")]
    public GameObject Portrait;
    public GameObject Name;
    public GameObject DialogueBOX;
    public GameObject Choice1;
    public GameObject Choice2;
    public GameObject Choice3;
    public GameObject PressSpaceToSkip;
    public GameObject PortraitAnchor;
    [Header("Texts")]
    public Text DialogueBoxTEXT;
    public Text Choice1Text;
    public Text Choice2Text;
    public Text Choice3Text;
    public Text NameText;
    [Header("idk")]
    public PortraitManager portratmanager; 

    void Start()
    {
        DialogueBOX.SetActive(false);
        portratmanager = this.gameObject.GetComponent<PortraitManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            portratmanager.ClearPortrait();
            DialogueBOX.SetActive(false);
            DialogueBoxTEXT.text = "";
        }
    }

    public void InitiateDialouge(DialogueSO dialogueSO)
    {
        portratmanager.CreatePortrait(dialogueSO);
        DialogueBOX.SetActive(true);
        DialogueBoxTEXT.text = dialogueSO.dialogues[0].person1Dialogue[0];
    }
}
