using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameObject inventory;

    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            if (!inventory.activeSelf)
            {
                inventory.SetActive(true); // Toggle inventory visibility
                Debug.Log("Inventory toggled!"); // Placeholder for actual inventory UI toggle
            }
            else
            {

                inventory.SetActive(false); // Toggle inventory visibility
                Debug.Log("Inventory toggled off!"); // Placeholder for actual inventory UI toggle
            }
        }

    }
}
