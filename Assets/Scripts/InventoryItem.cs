using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image image;
    public Transform parentAfterDrag;
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
}
