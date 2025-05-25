using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public int MaxStackedItems = 3; // Maximum number of items in a stack
    public GameObject inventory;
    public InventorySlot[] inventorySlots; // Array of inventory slots
    public GameObject inventoryItemPrefab; // Prefab for the inventory item

    int selectedSlot = 0; // Index of the currently selected slot


    public bool AddItem(Item item)
    {
        //Check if the item is on the inventory already
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null &&
                itemInSlot.item == item &&
                itemInSlot.countItem < MaxStackedItems &&
                itemInSlot.item.stackable) // Check if the slot is empty
            {
                itemInSlot.countItem++;
                itemInSlot.RefreshCount(); // Refresh the count display
                return true;
            }
        }
        //Find any empty Slot
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot == null) // Check if the slot is empty
            {
                SpawnNewItem(item, slot); // Spawn a new item in the empty slot
                return true;
            }
        }
        return false; // No empty slot found
    }

    // Update is called once per frame
    void SpawnNewItem(Item item, InventorySlot slot)
    {
        GameObject newItemGO = Instantiate(inventoryItemPrefab, slot.transform); // Instantiate the item prefab in the slot
        InventoryItem inventoryItem = newItemGO.GetComponent<InventoryItem>(); // Get the InventoryItem component
        inventoryItem.InitializeItem(item); // Initialize the item with the provided item
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
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
    public void ChangeSelectedSlot(int newValue)
    {
        if (selectedSlot >= 0)
        {
            inventorySlots[selectedSlot].Deselect(); // Deselect the previously selected slot
        }

        inventorySlots[newValue].Select(); // Deselect the previously selected slot
        selectedSlot = newValue; // Update the selected slot index
    }
    public Item GetSelectedItem()
    {
        Debug.Log("tete: " + selectedSlot);
        InventorySlot slot = inventorySlots[selectedSlot];
        InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
        if (itemInSlot != null)
        {
            return itemInSlot.item; // Return the item in the selected slot
        }
        return null; // No item in the selected slot
    }
}
