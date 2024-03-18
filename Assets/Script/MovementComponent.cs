using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementComponent : MonoBehaviour
{
    [SerializeField] float turnSpeed = 8;
    [SerializeField] Camera mainCam;

    void Start()
    {
        mainCam = Camera.main;
    }
    
    public float RotateTowards(Vector3 AimDir)
    {
        float currentTurnSpeed = 0;
        if (AimDir.magnitude != 0)
        {
            Quaternion prevRot = transform.rotation;

            float turnLerpAlpha = turnSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(AimDir, Vector3.up), turnLerpAlpha);

            Quaternion currentRot = transform.rotation;
            float Dir = Vector3.Dot(AimDir, transform.right) > 0 ? 1 : -1;
            float rotationDelta = Quaternion.Angle(prevRot, currentRot) * Dir;
            currentTurnSpeed = rotationDelta / Time.deltaTime;
        }
        return currentTurnSpeed;
    }

    public Vector3 InputToWorldDir(Vector2 inputVal)
    {
        Vector3 rightDir = mainCam.transform.right;
        Vector3 upDir = Vector3.Cross(rightDir, Vector3.up);
        return rightDir * inputVal.x + upDir * inputVal.y;
    }
}
