using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour
{
    private Vector3 _offset;
    [SerializeField] private Transform target;
    [SerializeField] private float smoothTime;
    private Vector3 _currentVelocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        // Calculate the initial offset
        _offset = transform.position - target.position;
    }

    // Update is called once per frame
    void Update()
    {
        // You can add any other relevant logic in the Update method
    }

    private void LateUpdate()
    {
        // Only update the target's position, not the rotation
        Vector3 targetPosition = target.position + _offset;

        // Smoothly move the camera to the new position
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _currentVelocity, smoothTime);
    }
}
