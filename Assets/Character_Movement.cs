using UnityEngine;

public class Character_Movement : MonoBehaviour
{
    private CharacterController character;
    private Animator animator;
    private Vector3 inputs;

    public float speed = 5f;

    void Start()
    {
        character = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        inputs.Set(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        character.Move(inputs * speed * Time.deltaTime);
        //character.Move(Physics.gravity * Time.deltaTime);

        if(inputs != Vector3.zero)
        {
            animator.SetBool("isWalking", true);
            transform.forward = Vector3.Slerp(transform.forward, inputs, Time.deltaTime * 10); 
            //animator.SetFloat("inputX", inputs.x);
            //animator.SetFloat("inputY", inputs.z);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }
}
