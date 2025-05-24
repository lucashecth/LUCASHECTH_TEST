using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image image;
    public Transform parentToReturnTo;
    public void OnBeginDrag(PointerEventData eventData)

    {
        // Handle the beginning of the drag
        image.raycastTarget = false; // Disable raycast target to allow dragging
        parentToReturnTo = transform.parent; // Store the parent to return to
        transform.SetParent(transform.root); // Set the parent to the root to allow free movement
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Handle the dragging
        Debug.Log("Dragging");
        transform.position = Input.mousePosition; // Update the position of the item to follow the mouse
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Handle the end of the drag
        image.raycastTarget = true;
        transform.SetParent(parentToReturnTo); // Return to the original parent
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
