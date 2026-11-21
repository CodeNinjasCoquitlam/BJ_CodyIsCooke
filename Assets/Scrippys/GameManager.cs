using UnityEngine;

public class GameManager : MonoBehaviour
{
    public ItemLookupTable[] ItemTable;

    [System.Serializable]
    public class ItemLookupTable
    {
        public GameObject ItemPrefab;
        public string ItemName;
    }
}
