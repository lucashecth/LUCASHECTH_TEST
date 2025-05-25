using UnityEngine;
using UnityEngine.EventSystems;

public class ForwardClickToParent : MonoBehaviour, IPointerClickHandler
{
    public InventoryItem inventoryItemParent;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (inventoryItemParent != null)
        {
            inventoryItemParent.OnItemClicked(eventData);
        }
    }
}
