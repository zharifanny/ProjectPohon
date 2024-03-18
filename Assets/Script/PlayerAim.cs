using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    #region Datamembers
    #region Editor Settings
    [SerializeField] private LayerMask groundMask; // The layermask that defines what is ground.
    [SerializeField] MovementComponent movementComponent;
    [SerializeField] float animTurnSpeed = 30f;
    #endregion // is the end of the editor settings region
    #region Private Fields
    private Camera mainCamera; // Cache the camera, Camera.main is an expensive operation.
    private Vector2 aimInput;
    private float animatorTurnSpeed;
    private Animator animator;
    #endregion // is the end of the private fields region
    #endregion // is the end of the datamembers region
    #region Methods
    #region Unity Callbacks
    private void Start()
    {
        // Cache the camera, Camera.main is an expensive operation.
        mainCamera = Camera.main;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Aim();
    }

    #endregion

    private void Aim()
    {
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
    }

    public void UpdateAim(Vector3 currentMoveDir)
    {
        Vector3 AimDir = currentMoveDir;
        if (aimInput.magnitude != 0)
        {
            AimDir = movementComponent.InputToWorldDir(aimInput);
        }
        RotateTowards(AimDir);
    }

    private void RotateTowards(Vector3 AimDir)
    {
        float currentTurnSpeed = movementComponent.RotateTowards(AimDir);
        animatorTurnSpeed = Mathf.Lerp(animatorTurnSpeed, currentTurnSpeed, Time.deltaTime * animTurnSpeed);

        animator.SetFloat("turnSpeed", animatorTurnSpeed);
    }

    #endregion
}