using UnityEngine;

public class DemoScript : MonoBehaviour
{
    public Inventory inventory; // Reference to the InventoryManager script
    public Item[] itensToPickup; // Item to add to the inventory

    // Update is called once per frame
    public void PickupItem(int Id)
    {
        bool result = inventory.AddItem(itensToPickup[Id]); // Attempt to add the item to the inventory
        if (result)
        {
            Debug.Log("Item added to inventory: " + itensToPickup[Id].name); // Log success
        }
        else
        {
            Debug.Log("Inventory full, could not add item: " + itensToPickup[Id].name); // Log failure
        }
    }
    public void GetSelectedItem()
    {
        Item recievedItem = inventory.GetSelectedItem(); // Get the currently selected item from the inventory
        if (recievedItem != null)
        {
            Debug.Log("Selected item: " + recievedItem.name); // Log the selected item
        }
        else
        {
            Debug.Log("No item selected!"); // Log if no item is selected
        }
    }
}
