using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Animator animator;
    Vector2 moveInput;
    Vector3 moveDir;
    float startTime;
    [SerializeField] CharacterController characterController; 
    // [SerializeField] Transform groundCheck;
    // [SerializeField] LayerMask ground;
    [SerializeField] AudioSource jumpSound;
    [SerializeField] MovementComponent movementComponent;
    [SerializeField] PlayerAim aimScript;
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float maxMoveSpeed = 80f; 
    [SerializeField] float minMoveSpeed = 5f; 
    [SerializeField] float dashSpeed = 30f; 
    [SerializeField] float dashTime = 0.2f; 
    [SerializeField] float dashCooldown = 2f; 
    [SerializeField] KeyCode dashInput = KeyCode.Space; 

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        PerformMove();
    }

    private void PerformMove()
    {
        moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        moveDir = movementComponent.InputToWorldDir(moveInput);

        characterController.Move(moveDir * Time.deltaTime * moveSpeed);

        float forward = Vector3.Dot(moveDir, transform.forward);
        float right = Vector3.Dot(moveDir, transform.right);

        animator.SetFloat("forwardSpeed", forward);
        animator.SetFloat("rightSpeed", right);

        characterController.Move(Vector3.down * Time.deltaTime * 10f);
        aimScript.UpdateAim(moveDir);

        if(Input.GetKeyDown(dashInput) && Time.time - startTime >= dashCooldown)
        {
            animator.SetTrigger("dash");
            StartCoroutine(Dash());
        }
    }

    private IEnumerator Dash()
    {
        startTime = Time.time;
        while(Time.time < startTime + dashTime)
        {
            characterController.Move(moveDir * Time.deltaTime * dashSpeed);
            yield return null;
        }
    }
}