using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Unity.VisualScripting;

public class Interact : MonoBehaviour
{
    [Header("Player")]
    public GameObject Codey;
    [Header("Text")]
    public GameObject TextObject;
    public Text InteractText;
    public string TextAction;
    [Header("Keys")]
    public KeyCode KeyCodeToPress;
    public string KeyCodeToPressString;
    [Header("Function")]
    public UnityEvent onInteract;
    [Header("Bool")]
    public bool IsInRange = false;
    [Header("IS MAYONNAISE AN INSTRUMENT?")]
    private DialogueManager gameymanagery;

    void Start()
    {
        TextObject.SetActive(false);
        gameymanagery = GameObject.Find("GameManager").GetComponent<DialogueManager>();
    }

    void Update()
    {
        if (IsInRange == true && Input.GetKeyDown(KeyCodeToPress) && gameymanagery.DialogueActive == false)
        {
            onInteract.Invoke();
        }
    }

    public void OnTriggerEnter(Collider Other)
    {
        if (Other.gameObject.CompareTag("Player"))
        {
            IsInRange = true;
            TextObject.SetActive(true);
            InteractText.text = "Press " + KeyCodeToPressString + " To " + TextAction;
        }
    }

    public void OnTriggerExit(Collider Other)
    {
        if (Other.gameObject.CompareTag("Player"))
        {
            IsInRange = false;
            InteractText.text = "";
            TextObject.SetActive(false);
        }
    }

}
