using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    #region Datamembers
    #region Editor Settings
    [SerializeField] private LayerMask groundMask; // The layermask that defines what is ground.
    #endregion // is the end of the editor settings region
    #region Private Fields
    private Camera mainCamera; // Cache the camera, Camera.main is an expensive operation.
    #endregion // is the end of the private fields region
    #endregion // is the end of the datamembers region
    #region Methods
    #region Unity Callbacks
    private void Start()
    {
        // Cache the camera, Camera.main is an expensive operation.
        mainCamera = Camera.main;
    }

    private void Update()
    {
        Aim();
    }

    #endregion

    private void Aim()
    {
        var (success, position) = GetMousePosition();
        if (success)
        {
            // Calculate the direction
            var direction = position - transform.position;

            // You might want to delete this line.
            // Ignore the height difference.
            direction.y = 0;

            // Make the transform look in the direction.
            transform.forward = direction;
        }
    }

    private (bool success, Vector3 position) GetMousePosition()
    {
        // Create a ray from the mouse position. 
        var ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        // Check if the ray hits something.
        if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, groundMask))
        {
            // The Raycast hit something, return with the position.
            return (success: true, position: hitInfo.point);
        }
        else
        {
            // The Raycast did not hit anything.
            return (success: false, position: Vector3.zero);
        }
    }

    #endregion
}