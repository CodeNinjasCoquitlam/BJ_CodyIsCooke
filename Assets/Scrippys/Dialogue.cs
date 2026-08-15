using UnityEngine;

public class Dialogue : MonoBehaviour
{

    [SerializeField] private DialogueSO dialogueSO;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log(dialogueSO.firstDialogue);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(dialogueSO.testDialogue);
        }
    }
}
