using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventory : MonoBehaviour
{
    public int MaxStackedItems = 3; // Maximum number of items in a stack
    public GameObject inventory;
    public InventorySlot[] inventorySlots; // Array of inventory slots
    public GameObject inventoryItemPrefab; // Prefab for the inventory item
    public ItemDetails itemDetails; // Reference to the ItemDetails script
    public GameObject deletePopUp; // Reference to the delete popup image
    public Button minusPopUp, plusPopUp; // Reference to the buttons in the delete popup
    public TMP_Text quantityText; // Reference to the text displaying the quantity in the delete popup
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

    void SpawnNewItem(Item item, InventorySlot slot)
    {
        GameObject newItemGO = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGO.GetComponent<InventoryItem>();
        inventoryItem.InitializeItem(item);

        // Procurar o ForwardClickToParent no objeto instanciado ou seus filhos
        ForwardClickToParent forwardClick = newItemGO.GetComponentInChildren<ForwardClickToParent>();
        if (forwardClick != null)
        {
            forwardClick.inventoryItemParent = inventoryItem; // atribui referência para o pai
            Debug.Log("ForwardClickToParent vinculado ao InventoryItem com sucesso");
        }
        else
        {
            Debug.LogWarning("ForwardClickToParent não encontrado no prefab instanciado!");
        }
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

    public Item GetSelectedItem() // DAR UMA OLHADA QUE DEVE DAR PRA TIRAR 
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
    public void RemoveItem()
    {
        InventoryItem itemToDestroy = itemDetails.itemPosition.GetComponentInChildren<InventoryItem>();
        int itemQuantity = itemDetails.itemQuantity; // Get the current quantity text

        if (itemQuantity == int.Parse(quantityText.text))
        {
            if (itemToDestroy != null)
            {
                Destroy(itemToDestroy.gameObject); // Destroi o GameObject do item
            }
            else
            {
                Debug.LogWarning("Nenhum InventoryItem encontrado no slot para remover.");
            }

            itemDetails.ClearDetails(); // Limpa os detalhes
            deletePopUp.SetActive(false); // Fecha o pop-up
        }
        else if(int.Parse(quantityText.text)< itemQuantity && itemToDestroy != null)
        {
            itemToDestroy.countItem -= int.Parse(quantityText.text); // Decrementa a quantidade do item
            itemToDestroy.RefreshCount(); // Atualiza a contagem do item
            itemDetails.ClearDetails(); // Limpa os detalhes
            ClearDeleteSlot();
            deletePopUp.SetActive(false); // Fecha o pop-up
        }

    }
    public void ClearDeleteSlot()
    {
        int itemQuantity = itemDetails.itemQuantity;
        quantityText.text = "1"; // Reset the quantity text to 1
        if (itemQuantity == 1)
        {
            minusPopUp.interactable = false; // Disable the minus button
            plusPopUp.interactable = false; // Enable the plus button
        }
        else if(itemQuantity > 1 && (itemQuantity < MaxStackedItems|| itemQuantity == MaxStackedItems))
        {
            minusPopUp.interactable = false; // Disable the plus button
            plusPopUp.interactable = true; // Enable the plus button
        }
    }


    public void QuantityToRemove(string whichBTNIsPressed)
    {
        int quantity = int.Parse(quantityText.text); // Get the current quantity text
        int itemQuantity = itemDetails.itemQuantity;
        if (whichBTNIsPressed == "minus")
        {
            if (itemQuantity == 1)
            {
                minusPopUp.interactable = false; // Disable the minus button
                plusPopUp.interactable = false; // Enable the plus button
            }
            Debug.Log("itemQuantity: " + itemQuantity);
            Debug.Log("quantidade: " + quantity);
            if (itemQuantity > 1)
            {
                minusPopUp.interactable = true;
                quantity--;
                if (quantity == 1)
                {
                    minusPopUp.interactable = false; // Disable the minus button
                    plusPopUp.interactable = true; // Enable the plus button
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
                minusPopUp.interactable = true; // Disable the plus button
                if (quantity == itemQuantity)
                {
                    plusPopUp.interactable = false; // Disable the plus button
                    minusPopUp.interactable = true; // Enable the minus button
                }
            }
            Debug.Log("itemQuantity: " + itemQuantity);
        }
        quantityText.text = quantity.ToString(); // Update the quantity text
    }
}
