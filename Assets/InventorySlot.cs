using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public Image image; // Reference to the Image component for the slot
    public Color selectedColor, notSelectedColor;
    public void Awake()
    {
        Deselect(); // Set the initial color to not selected
    }

    public void Select()
    {
        image.color = selectedColor; // Change the color to selected color
    }
    public void Deselect()
    {
        image.color = notSelectedColor; // Change the color to not selected color
    }
    
    public void OnDrop(PointerEventData eventData)
    {
        if(transform.childCount == 0)
        {
            InventoryItem inventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();
            inventoryItem.parentAfterDrag = transform;
        }
    }
}
