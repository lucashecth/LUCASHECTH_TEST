using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemDetails : MonoBehaviour
{
    public Image itemImage; // Reference to the image component
    public TMP_Text itemNameText; // Reference to the text component for the item name
    public GameObject itemPosition; // Reference to the transform for positioning the details panel
    public int itemQuantity; // Quantity of the item
    public GameObject useButton;

    public void FillDetails(Sprite sprite, string itemName, GameObject positionAtList, int itemCount)
    {
        itemImage.sprite = sprite; // Set the image sprite
        itemNameText.text = itemName; // Set the item name text
        itemPosition = positionAtList; // Set the position of the details panel
        itemQuantity = itemCount; // Set the item quantity
    }
    public void ClearDetails()
    {
        itemImage.sprite = null; // Clear the image sprite
        itemNameText.text = ""; // Clear the item name text
    }
    public void ShowDetails(Item item, int quantity)
    {
        if (item.itemType == Item.ItemType.HealthItem || item.actionType == Item.ActionType.Oxygen)
        {
            useButton.SetActive(true);
        }
        else
        {
            useButton.SetActive(false);
        }
    }

}
