using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemDetails : MonoBehaviour
{
    public Image itemImage;
    public TMP_Text itemNameText;
    public GameObject itemPosition;
    public int itemQuantity;
    public GameObject useButton;
    public Sprite defaultSprite; 

    public void FillDetails(Sprite sprite, string itemName, GameObject positionAtList, int itemCount)
    {
        itemImage.sprite = sprite; 
        itemNameText.text = itemName; 
        itemPosition = positionAtList; // Set the position of the details panel
        itemQuantity = itemCount; 
    }
    public void ClearDetails()
    {
        itemImage.sprite = defaultSprite;
        itemNameText.text = ""; 
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
