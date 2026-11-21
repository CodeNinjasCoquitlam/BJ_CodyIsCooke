using UnityEngine;

public class Container : MonoBehaviour
{
    public int ThisContainerSlotID;
    public GameObject ThisContainerItemAnchor;
    private GameManager GameyMangery;

    public void DisplayItemOnContainer(int ItemIndex)
    {
        Instantiate(GameyMangery.ItemTable[ItemIndex].ItemPrefab, ThisContainerItemAnchor.transform);
    }

    // Update is called once per frame
    void Start()
    {
        GameyMangery = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
}
