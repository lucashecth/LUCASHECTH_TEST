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
            if (Input.GetKey(KeyCode.LeftShift))
            {
                StopAllPreviousAnimations();
                animator.SetBool("isRunning", true);
                speed = 5f;
                transform.forward = Vector3.Slerp(transform.forward, inputs, Time.deltaTime * 10);
            }
            else
            {
                StopAllPreviousAnimations();
                animator.SetBool("isWalking", true);
                speed = 1.3f;
                transform.forward = Vector3.Slerp(transform.forward, inputs, Time.deltaTime * 10);
            }
        }
        else
        {
            StopAllPreviousAnimations();
        }
    }
    public void StopAllPreviousAnimations()
    {
        animator.SetBool("isWalking", false);
        animator.SetBool("isRunning", false);
    }
    public void PickingItemFromFloor()
    {
        StopAllPreviousAnimations();
        animator.SetTrigger("isPicking");
    }
}
