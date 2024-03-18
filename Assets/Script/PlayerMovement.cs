using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Animator animator;
    Vector2 moveInput;
    [SerializeField] CharacterController characterController; 
    // [SerializeField] Transform groundCheck;
    // [SerializeField] LayerMask ground;
    [SerializeField] AudioSource jumpSound;
    [SerializeField] MovementComponent movementComponent;
    [SerializeField] PlayerAim aimScript;
    [SerializeField] float moveSpeed = 20f;
    [SerializeField] float maxMoveSpeed = 80f; 
    [SerializeField] float minMoveSpeed = 5f; 

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

        Vector3 MoveDir = movementComponent.InputToWorldDir(moveInput);

        characterController.Move(MoveDir * Time.deltaTime * moveSpeed);

        float forward = Vector3.Dot(MoveDir, transform.forward);
        float right = Vector3.Dot(MoveDir, transform.right);

        animator.SetFloat("forwardSpeed", forward);
        animator.SetFloat("rightSpeed", right);

        characterController.Move(Vector3.down * Time.deltaTime * 10f);
        aimScript.UpdateAim(MoveDir);
    }
}