using NUnit.Framework.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image image;
    public Transform parentAfterDrag;
    public Item item; // Reference to the Item scriptable object
    public int countItem = 1; // Count of items in the stack
    public Text countText; // Text to display the item count

    //public ItemDetails itemDetails; // Reference to the InventorySlot script
    public GameObject itemDetailsGO;

    public void InitializeItem(Item newItem)
    {
        item = newItem; // Assign the new item to the current item
        image.sprite = newItem.image; // Set the image sprite from the item
        itemDetailsGO = GameObject.Find("InventoryEngine"); // Fill the item details with the new item
        parentAfterDrag = transform.parent;
        RefreshCount(); // Refresh the count display    
    }
    public void RefreshCount()
    {
        countText.text = countItem.ToString(); // Update the text to display the current item count
        bool textActive = countItem > 1; // Check if the count is greater than 1
        countText.gameObject.SetActive(textActive); // Set the text active or inactive based on the count
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        image.raycastTarget = false; // Disable raycast target to allow dragging
        parentAfterDrag = transform.parent; // Store the parent to return to
        transform.SetParent(transform.root); // Set the parent to the root to allow free movement
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Handle the dragging
        transform.position = Input.mousePosition; // Update the position of the item to follow the mouse
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Handle the end of the drag
        image.raycastTarget = true;
        transform.SetParent(parentAfterDrag); // Return to the original parent
    }
    public void OnItemClicked(PointerEventData eventData)
    {
        Debug.Log("Item clicked: " + item.name);
        int countItem = parentAfterDrag.GetComponentInChildren<InventoryItem>().countItem;
        itemDetailsGO.GetComponent<ItemDetails>().FillDetails(item.image, item.name, parentAfterDrag.gameObject,countItem); // Fill the item details with the current item
        itemDetailsGO.GetComponent<Inventory>().ClearDeleteSlot();
        itemDetailsGO.GetComponent<Inventory>().deletePopUp.SetActive(false); // Hide the delete popup
        
    }
}
