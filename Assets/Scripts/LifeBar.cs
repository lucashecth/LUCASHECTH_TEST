using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LifeBar : MonoBehaviour
{
    public Image fillImage;
    private float maxTime = 300f; // 5 minutos
    private float currentTime;
    public Animator animator;
    public ItemDetails itemDetails;
    public Inventory inventory;

    void Start()
    {
        currentTime = maxTime;
    }

    void Update()
    {
        if (animator.GetBool("isRunning") == true)
        {
            currentTime -= Time.deltaTime * 4;
            currentTime = Mathf.Clamp(currentTime, 0, maxTime);
            fillImage.fillAmount = currentTime / maxTime;
        }
        else
        {
            currentTime -= Time.deltaTime;
            currentTime = Mathf.Clamp(currentTime, 0, maxTime);
            fillImage.fillAmount = currentTime / maxTime;
        }
    }
    public void UseItem(string itemName)
    {
        if (itemName == "O2")
        {
            currentTime = currentTime + 60f; // Add 60 seconds to the current time
            fillImage.fillAmount = currentTime / maxTime;

            InventoryItem itemToDestroy = itemDetails.itemPosition.GetComponentInChildren<InventoryItem>();
            itemToDestroy.countItem -= 1;

            if (itemToDestroy.countItem <= 0)
            {
                Destroy(itemToDestroy.gameObject); // Remove o item do inventário
            }
            else
            {
                itemToDestroy.RefreshCount(); // Apenas atualiza o texto se ainda houver unidades
            }

            itemDetails.ClearDetails();
            inventory.ClearDeleteSlot();

        }
    }

}
