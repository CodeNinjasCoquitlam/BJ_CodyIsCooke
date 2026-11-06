using UnityEngine;

public class InteractITEM : MonoBehaviour
{
    private GameObject clone;
    public bool isholding;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (clone != null)
        {
            clone.transform.position = this.gameObject.transform.position + (transform.forward * 2f) + new Vector3(0,1,0);
        }
    }

    public void PickupItem(GameObject prefab)
    {
        if (!isholding)
        {
            clone = Instantiate(prefab);
            isholding = true;
        }
    }
}
