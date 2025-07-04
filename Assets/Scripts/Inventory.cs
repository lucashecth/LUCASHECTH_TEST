using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using static SaveAndLoad;

public class Inventory : MonoBehaviour
{
    public int MaxStackedItems = 3; // Maximum number of items in a stack
    public GameObject inventory, menu;
    public InventorySlot[] inventorySlots; // Array of inventory slots
    public GameObject inventoryItemPrefab; // Prefab for the inventory item
    public ItemDetails itemDetails; // Reference to the ItemDetails script
    public GameObject deletePopUp; // Reference to the delete popup image
    public Button minusPopUp, plusPopUp; // Reference to the buttons in the delete popup
    public TMP_Text quantityText; // Reference to the text displaying the quantity in the delete popup
    public ItemDatabase itemDatabase; // Reference to the ItemDatabase script

    int selectedSlot = 0; // Index of the currently selected slot

    private string savePath => Application.persistentDataPath + "/inventorySave.json";
    void Awake()
    {
        itemDatabase.Initialize();
        LoadInventory();
    }
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

    void SpawnNewItem(Item item, InventorySlot slot)
    {
        GameObject newItemGO = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGO.GetComponent<InventoryItem>();
        inventoryItem.InitializeItem(item);

        // Procurar o ForwardClickToParent no objeto instanciado ou seus filhos
        ForwardClickToParent forwardClick = newItemGO.GetComponentInChildren<ForwardClickToParent>();
        if (forwardClick != null)
        {
            forwardClick.inventoryItemParent = inventoryItem; // atribui referÍncia para o pai
        }
    }
    public void SaveInventory()
    {
        InventoryData data = new InventoryData();
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventoryItem item = inventorySlots[i].GetComponentInChildren<InventoryItem>();
            if (item != null)
            {
                data.items.Add(new ItemData
                {
                    itemID = item.item.id.ToString(),
                    count = item.countItem,
                    slotIndex = i
                });
            }
        }
        File.WriteAllText(savePath, JsonUtility.ToJson(data, true));
        Debug.Log("Inventory saved in: " + savePath);
    }
    public void LoadInventory()
    {
        if (!File.Exists(savePath)) return;

        string json = File.ReadAllText(savePath);
        InventoryData data = JsonUtility.FromJson<InventoryData>(json);

        foreach (var slot in inventorySlots)
        {
            InventoryItem existing = slot.GetComponentInChildren<InventoryItem>();
            if (existing != null) Destroy(existing.gameObject);
        }

        foreach (ItemData itemData in data.items)
        {
            Item item = itemDatabase.GetItemByID(int.Parse(itemData.itemID));
            if (item != null && itemData.slotIndex >= 0 && itemData.slotIndex < inventorySlots.Length)
            {
                GameObject newItem = Instantiate(inventoryItemPrefab, inventorySlots[itemData.slotIndex].transform);
                InventoryItem invItem = newItem.GetComponent<InventoryItem>();
                invItem.InitializeItem(item);
                invItem.countItem = itemData.count;
                invItem.RefreshCount();

                ForwardClickToParent forwardClick = newItem.GetComponentInChildren<ForwardClickToParent>();
                if (forwardClick != null)
                {
                    forwardClick.inventoryItemParent = invItem;
                }
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (!inventory.activeSelf)
            {
                inventory.SetActive(true);
            }
            else
            {
                inventory.SetActive(false);
            }
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (!menu.activeSelf)
            {
                menu.SetActive(true);
            }
            else
            {
                menu.SetActive(false);
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
        InventorySlot slot = inventorySlots[selectedSlot];
        InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
        if (itemInSlot != null)
        {
            return itemInSlot.item; // Return the item in the selected slot
        }
        return null; // No item in the selected slot
    }
    public void RemoveItem()
    {
        InventoryItem itemToDestroy = itemDetails.itemPosition.GetComponentInChildren<InventoryItem>();
        int itemQuantity = itemDetails.itemQuantity; // Get the current quantity text

        if (itemQuantity == int.Parse(quantityText.text))
        {
            if (itemToDestroy != null)
            {
                Destroy(itemToDestroy.gameObject); 
            }
            else
            {
                Debug.LogWarning("Nenhum InventoryItem encontrado no slot para remover.");
            }

            itemDetails.ClearDetails(); 
            deletePopUp.SetActive(false);
        }
        else if(int.Parse(quantityText.text)< itemQuantity && itemToDestroy != null)
        {
            itemToDestroy.countItem -= int.Parse(quantityText.text); 
            itemToDestroy.RefreshCount(); 
            itemDetails.ClearDetails(); 
            ClearDeleteSlot();
            deletePopUp.SetActive(false); 
        }

    }
    public void ClearDeleteSlot()
    {
        int itemQuantity = itemDetails.itemQuantity;
        quantityText.text = "1"; // Reset the quantity text to 1
        if (itemQuantity == 1)
        {
            minusPopUp.interactable = false; 
            plusPopUp.interactable = false; 
        }
        else if(itemQuantity > 1 && (itemQuantity < MaxStackedItems|| itemQuantity == MaxStackedItems))
        {
            minusPopUp.interactable = false; 
            plusPopUp.interactable = true; 
        }
    }


    public void QuantityToRemove(string whichBTNIsPressed)
    {
        int quantity = int.Parse(quantityText.text);
        int itemQuantity = itemDetails.itemQuantity;
        if (whichBTNIsPressed == "minus")
        {
            if (itemQuantity == 1)
            {
                minusPopUp.interactable = false;
                plusPopUp.interactable = false;
            }
            Debug.Log("itemQuantity: " + itemQuantity);
            Debug.Log("quantidade: " + quantity);
            if (itemQuantity > 1)
            {
                minusPopUp.interactable = true;
                quantity--;
                if (quantity == 1)
                {
                    minusPopUp.interactable = false;
                    plusPopUp.interactable = true;
                }
            }
            Debug.Log("itemQuantity: " + itemQuantity);
        }
        if (whichBTNIsPressed == "plus")
        {
            Debug.Log("itemQuantity: " + itemQuantity);
            if (quantity < itemQuantity)
            {
                quantity++;
                minusPopUp.interactable = true;
                if (quantity == itemQuantity)
                {
                    plusPopUp.interactable = false;
                    minusPopUp.interactable = true;
                }
            }
            Debug.Log("itemQuantity: " + itemQuantity);
        }
        quantityText.text = quantity.ToString(); // Update the quantity text
    }
    public void SaveGame()
    {
        SaveInventory();
    }

    public void LoadGame()
    {
        LoadInventory();
    }
}
