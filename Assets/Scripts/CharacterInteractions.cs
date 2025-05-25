using UnityEngine;

public class CharacterInteractions : MonoBehaviour
{
    public Collider interactableCollider;
    public Inventory inventory; // Reference to the InventoryManager script
    public Item[] itensToPickup; // Item to add to the inventory
    public Character_Movement characterMovement; // Reference to the Character_Movement script

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Verifica se o objeto que entrou tem a tag "Player"
        {
            Debug.Log("Jogador entrou no collider!");
        }
    }

    // Método chamado enquanto o jogador está dentro do collider
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                PickupItem(1); // Chama o método PickupItem com o ID do item desejado
                Debug.Log("Jogador pressionou a tecla E dentro do collider!");
                characterMovement.PickingItemFromFloor();
                Destroy(this.gameObject); // Destroi o objeto após pegar o item
            }
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
