using TMPro;
using UnityEngine;
using System.Collections;

public class RobotQuestChecker : MonoBehaviour
{
    public GameObject questHolder;
    public Inventory inventory;
    public string appleID;
    public string bananaID;
    public string mangoID;

    public GameObject endGameScreen;
    private CanvasGroup endScreenCanvasGroup;
    private bool questComplete = false;

    private void Start()
    {
        if (endGameScreen != null)
        {
            endScreenCanvasGroup = endGameScreen.GetComponent<CanvasGroup>();
            if (endScreenCanvasGroup != null)
            {
                endScreenCanvasGroup.alpha = 0f;
                endGameScreen.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!this.isActiveAndEnabled) return;
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
                questHolder.SetActive(true);
                questHolder.GetComponentInChildren<TMP_Text>().text = "All items have been collected, you made a great job! \nYou're hired!!!";

                if (endGameScreen != null)
                {
                    StartCoroutine(ShowEndScreenWithFade());
                }
            }
        }
        else
        {
            questHolder.SetActive(true);
            string missing = "You still need: ";
            if (!hasApple) missing += "Apple ";
            if (!hasBanana) missing += "Banana ";
            if (!hasMango) missing += "Mango";
            questHolder.GetComponentInChildren<TMP_Text>().text = missing;
        }
    }

    private IEnumerator ShowEndScreenWithFade()
    {
        yield return new WaitForSeconds(2f); // Espera 2 segundos

        endGameScreen.SetActive(true);

        float duration = 1.5f;
        float time = 0f;
        while (time < duration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, time / duration);
            endScreenCanvasGroup.alpha = alpha;
            yield return null;
        }
        endScreenCanvasGroup.alpha = 1f;
    }
}
