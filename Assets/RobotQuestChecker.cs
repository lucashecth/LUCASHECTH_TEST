using UnityEngine;

public class RobotQuestChecker : MonoBehaviour
{
    public Inventory inventory; // Referência ao inventário
    public string appleID = "1";
    public string bananaID = "2";
    public string mangoID = "3";

    public GameObject endGameScreen; // Tela de fim de jogo (se tiver)

    private bool questComplete = false;

    private void OnTriggerEnter(Collider other)
    {
        if (this.GetComponent<RobotQuestChecker>().isActiveAndEnabled)
        {
            if (!other.CompareTag("Player")) return;

            bool hasApple = false;
            bool hasBanana = false;
            bool hasMango = false;

            foreach (InventorySlot slot in inventory.inventorySlots)
            {
                InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
                if (itemInSlot == null) continue;

                string itemID = itemInSlot.item.id.ToString();

                if (itemID == appleID) hasApple = true;
                if (itemID == bananaID) hasBanana = true;
                if (itemID == mangoID) hasMango = true;
            }

            if (hasApple && hasBanana && hasMango)
            {
                if (!questComplete)
                {
                    questComplete = true;
                    Debug.Log("✅ All Itens has been collected");
                    if (endGameScreen != null)
                    {
                        endGameScreen.SetActive(true); // Mostrar tela de fim de jogo
                    }
                }
            }
            else
            {
                string missing = "You still need: ";
                if (!hasApple) missing += "🍎 Apple ";
                if (!hasBanana) missing += "🍌 Banana ";
                if (!hasMango) missing += "🥭 Mango ";

                Debug.Log(missing);
            }
        }
    }
}
