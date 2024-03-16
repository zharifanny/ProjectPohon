using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Animator animator;
    Vector2 moveInput;
    Vector2 aimInput;
    Camera mainCam;
    float animatorTurnSpeed;
    [SerializeField] CharacterController characterController; 
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask ground;
    [SerializeField] AudioSource jumpSound;
    [SerializeField] MovementComponent movementComponent;
    [SerializeField] float moveSpeed = 20f;
    [SerializeField] float maxMoveSpeed = 80f; 
    [SerializeField] float minMoveSpeed = 5f; 
    [SerializeField] float animTurnSpeed = 30f;

    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        PerformMoveAndAim();
    }



    private void OnCollisionEnter(Collision collision)
    {
   
    }

    Vector3 InputToWorldDir(Vector2 inputVal)
    {
        Vector3 rightDir = mainCam.transform.right;
        Vector3 upDir = Vector3.Cross(rightDir, Vector3.up);
        return rightDir * inputVal.x + upDir * inputVal.y;
    }

    private void PerformMoveAndAim()
    {
        moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if(Input.GetKey(KeyCode.Mouse1))
        {
            Vector2 screenSize = new Vector2(Screen.width, Screen.height);
            Vector2 screenPoint = Input.mousePosition;
            aimInput = (screenPoint - (screenSize / 2f)).normalized;
        }
        else
        {
            aimInput = new Vector2();
        }

        Vector3 MoveDir = InputToWorldDir(moveInput);

        characterController.Move(MoveDir * Time.deltaTime * moveSpeed);

        UpdateAim(MoveDir);

        float forward = Vector3.Dot(MoveDir, transform.forward);
        float right = Vector3.Dot(MoveDir, transform.right);

        animator.SetFloat("forwardSpeed", forward);
        animator.SetFloat("rightSpeed", right);

        characterController.Move(Vector3.down * Time.deltaTime * 10f);
    }

    private void UpdateAim(Vector3 currentMoveDir)
    {
        Vector3 AimDir = currentMoveDir;
        if (aimInput.magnitude != 0)
        {
            AimDir = InputToWorldDir(aimInput);
        }
        RotateTowards(AimDir);
    }

    private void RotateTowards(Vector3 AimDir)
    {
        float currentTurnSpeed = movementComponent.RotateTowards(AimDir);
        animatorTurnSpeed = Mathf.Lerp(animatorTurnSpeed, currentTurnSpeed, Time.deltaTime * animTurnSpeed);

        animator.SetFloat("turnSpeed", animatorTurnSpeed);
    }
}