using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LifeBar : MonoBehaviour
{
    public Image fillImage;
    private float maxTime = 300f; // 5 minutos
    private float currentTime;
    //public Character_Movement playerMovement;
    public Animator animator;

    void Start()
    {
        currentTime = maxTime;
    }

    void Update()
    {
        if (animator.GetBool("isRunning") == true)
        {
            currentTime -= Time.deltaTime * 2;
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
        }
    }

}
