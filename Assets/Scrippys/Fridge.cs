using UnityEngine;

public class Fridge : MonoBehaviour
{
    public GameObject FridgeUI;
    private InteractITEM interactionScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        FridgeUI.SetActive(false);
        interactionScript = GameObject.Find("Codey Variant").GetComponent<InteractITEM>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseFridge();
        }
    }
    public void OpenFridge()
    {
        FridgeUI.SetActive(true);
    }
    public void GrabItem(int ItemID)
    {
        interactionScript.GiveItemToCodey(ItemID);
        CloseFridge();
    }
    public void CloseFridge()
    {
        FridgeUI.SetActive(false);
    }
}
