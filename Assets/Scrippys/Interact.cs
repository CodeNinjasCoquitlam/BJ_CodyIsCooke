using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

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

    void Start()
    {
        TextObject.SetActive(false);
    }

    void Update()
    {
        if (IsInRange == true && Input.GetKeyDown(KeyCodeToPress))
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
