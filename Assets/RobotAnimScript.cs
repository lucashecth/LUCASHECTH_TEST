using TMPro;
using UnityEngine;

public class RobotAnimScript : MonoBehaviour
{
    public Animator robotAnimator;
    public string playerTag = "Player";
    public bool isOnTrigger = false;
    public TMP_Text text;
    public GameObject chat1;
    public GameObject pressButtonGO;

    private void Start()
    {
        if (robotAnimator == null)
        {
            robotAnimator = GetComponent<Animator>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            robotAnimator.SetBool("isWaving", true);
            isOnTrigger = true;
            pressButtonGO.SetActive(true);
            text.text = "Press E to talk to C186";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            robotAnimator.SetBool("isWaving", false);
            pressButtonGO.SetActive(false);
            isOnTrigger = false;
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isOnTrigger)
        {
            robotAnimator.SetBool("isTalking", true);
            isOnTrigger = false;
            chat1.SetActive(true);
        }
    }

    public void StopTalking()
    {
        robotAnimator.SetBool("isTalking", false);
        robotAnimator.SetBool("isWaving", false);
        chat1.SetActive(false);
        isOnTrigger = true;
    }
}
