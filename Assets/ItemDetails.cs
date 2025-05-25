using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemDetails : MonoBehaviour
{
    public Image itemImage; // Reference to the image component
    public TMP_Text itemNameText; // Reference to the text component for the item name

    public void FillDetails(Sprite sprite, string itemName)
    {
        itemImage.sprite = sprite; // Set the image sprite
        itemNameText.text = itemName; // Set the item name text
    }
}
