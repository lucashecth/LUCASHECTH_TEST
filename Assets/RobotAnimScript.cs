using TMPro;
using UnityEngine;

public class RobotAnimScript : MonoBehaviour
{
    public Animator robotAnimator;          // Refer�ncia para o Animator do rob�
    //public string animationTriggerName = "Activate"; // Nome do trigger na anima��o
    public string playerTag = "Player";     // Tag do jogador
    public bool isOnTrigger = false; // Flag para verificar se o jogador est� no trigger
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
            pressButtonGO.SetActive(true); // Ativa o objeto que cont�m o texto "Press E to talk to C186"
            text.text = "Press E to talk to C186"; // Exibe a mensagem quando o jogador entra no trigger
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            robotAnimator.SetBool("isWaving", false);
            pressButtonGO.SetActive(false); // Desativa o objeto que cont�m o texto "Press E to talk to C186"
            isOnTrigger = false;
        }
    }
    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && isOnTrigger)
        {
            robotAnimator.SetBool("isTalking", true);
            isOnTrigger = false; // Desativa o trigger ap�s a ativa��o
            chat1.SetActive(true); // Ativa o chat
        }
    }
    public void StopTalking()
    {
        robotAnimator.SetBool("isTalking", false);
        robotAnimator.SetBool("isWaving", false); // Para a anima��o de aceno
        chat1.SetActive(false); // Desativa o chat
        isOnTrigger = true; // Reativa o trigger para permitir nova intera��o
    }
}
