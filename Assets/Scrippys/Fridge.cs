using UnityEngine;

public class Fridge : MonoBehaviour
{
    public GameObject FridgeUI;
    public GameObject[] FoodsToTakeOut;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        FridgeUI.SetActive(false);
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
        //codey.pickupItem(FoodsToTakeOut[itemArrayID])
    }
    public void CloseFridge()
    {
        FridgeUI.SetActive(false);
    }
}
