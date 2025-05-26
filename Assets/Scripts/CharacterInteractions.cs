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
            switch (itemType)
            {
                case "Oxygen":
                    PressToDo.GetComponent<TextMeshProUGUI>().text = "Press E to pick up " + itensToPickup[0].name;
                    break;
                case "Water":
                    PressToDo.GetComponent<TextMeshProUGUI>().text = "Press E to pick up " + itensToPickup[1].name;
                    break;
                case "Apple":
                    PressToDo.GetComponent<TextMeshProUGUI>().text = "Press E to pick up " + itensToPickup[2].name;
                    break;
                case "Banana":
                    PressToDo.GetComponent<TextMeshProUGUI>().text = "Press E to pick up " + itensToPickup[3].name;
                    break;
                case "Mango":
                    PressToDo.GetComponent<TextMeshProUGUI>().text = "Press E to pick up " + itensToPickup[4].name;
                    break;
            }


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
            PressToDo.SetActive(false);
            Destroy(this.gameObject, 0.4f);
        }
    }
    public void PickupItem(int Id)
    {
        bool result = inventory.AddItem(itensToPickup[Id]); // Attempt to add the item to the inventory
    }

    public void GetSelectedItem()
    {
        Item recievedItem = inventory.GetSelectedItem(); // Get the currently selected item from the inventory
    }
}
