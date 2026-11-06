using UnityEngine;

public class Fridge : MonoBehaviour
{
    public GameObject FridgeUI;
    public GameObject[] FoodsToTakeOut;
    private InteractITEM cody;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        FridgeUI.SetActive(false);
        cody = GameObject.Find("Codey Variant").GetComponent<InteractITEM>();
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
    public void GrabItem(int itemArrayID)
    {
        cody.PickupItem(FoodsToTakeOut[itemArrayID]);
        CloseFridge();
    }
    public void CloseFridge()
    {
        FridgeUI.SetActive(false);
    }
}
