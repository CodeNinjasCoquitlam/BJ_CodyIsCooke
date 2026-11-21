using Unity.VisualScripting;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.UIElements;

public class InteractITEM : MonoBehaviour
{
    private GameObject clone;
    public bool isholding;
    private GameManager GameyMangery;
    private GameObject HeldItem;
    private int HeldItemIndex;
    private GameObject currentCodeyHeldItem;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameyMangery = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void SetItem(int ItemIndex, int SlotIndex)
    {
        
    }
    public void GiveItemToCodey(int ItemIndex)
    {
        HeldItem = GameyMangery.ItemTable[ItemIndex].ItemPrefab;
        HeldItemIndex = ItemIndex;
    }
    public void PickupItem(int ItemIndex, int SlotIndex)
    {

    }
    public void PutDownItem(int SlotIndex)
    {
        SetItem(SlotIndex, HeldItemIndex);
        //Item Index 0 is air
        //Slot Index 0 is codey's Hands
        SetItem(0, 0);
    }
    public void DisplayHeldItem()
    {
        Vector3 spawnPos = transform.position + (transform.forward * 2f) + new Vector3(0f, 1f, 0f);
        currentCodeyHeldItem = Instantiate(HeldItem, spawnPos, Quaternion.identity);
    }
    void Update()
    {
        currentCodeyHeldItem.transform.position = transform.position + (transform.forward * 2f) + new Vector3(0f, 1f, 0f);
    }
}
