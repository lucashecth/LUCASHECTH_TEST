using System.Drawing;
using TMPro;
using UnityEngine;

public class CharacterInteractions : MonoBehaviour
{
    public Collider interactableCollider;
    public Inventory inventory; // Reference to the InventoryManager script
    public Item[] itensToPickup; // Item to add to the inventory
    public Character_Movement characterMovement; // Reference to the Character_Movement script
    public bool isOnTrigger = false;
    public GameObject PressToDo;
    public string itemType;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PressToDo.SetActive(true);
            PressToDo.GetComponent<TextMeshProUGUI>().text = "Press E to pick up " + itensToPickup[0].name;
            isOnTrigger = true;
}
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PressToDo.SetActive(false);
            isOnTrigger = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isOnTrigger = true;
        }
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isOnTrigger)
        {
            switch (itemType)
            {
                case "Oxygen":
                    PickupItem(0);
                    break;
                case "Water":
                    PickupItem(1);
                    break;
                case "Apple":
                    PickupItem(2);
                    break;
                case "Banana":
                    PickupItem(3);
                    break;
                case "Mango":
                    PickupItem(4);
                    break;
            }
                characterMovement.PickingItemFromFloor();
                Destroy(this.gameObject);
        }
    }
    public void PickupItem(int Id)
    {
        bool result = inventory.AddItem(itensToPickup[Id]); // Attempt to add the item to the inventory
        if (result)
        {
            Debug.Log("Item added to inventory: " + itensToPickup[Id].name); // Log success
        }
        else
        {
            Debug.Log("Inventory full, could not add item: " + itensToPickup[Id].name); // Log failure
        }
    }
    public void GetSelectedItem()
    {
        Item recievedItem = inventory.GetSelectedItem(); // Get the currently selected item from the inventory
        if (recievedItem != null)
        {
            Debug.Log("Selected item: " + recievedItem.name); // Log the selected item
        }
        else
        {
            Debug.Log("No item selected!"); // Log if no item is selected
        }
    }
}
