using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caméra : MonoBehaviour
{
    public Camera cam;                // Main camera
    public GameObject[] Lcam;         // List of camera positions/rotations
    private int index = 0;            // Current camera index
    public float moveSpeed = 5f;      // Speed of position interpolation
    public float rotateSpeed = 5f;    // Speed of rotation interpolation
    private Vector3 targetPosition;   // Target position for smooth movement
    private Quaternion targetRotation; // Target rotation for smooth rotation

    void Start()
    {
        // Activate the first camera position
        if (Lcam.Length > 0)
        {
            SetTarget(0);
            cam.transform.position = targetPosition;
            cam.transform.rotation = targetRotation;
        }
    }

    void Update()
    {
        // Smoothly move the camera towards the target position and rotation
        cam.transform.position = Vector3.Lerp(cam.transform.position, targetPosition, Time.deltaTime * moveSpeed);
        cam.transform.rotation = Quaternion.Slerp(cam.transform.rotation, targetRotation, Time.deltaTime * rotateSpeed);

        // Switch to the previous camera (Q)
        if (Input.GetKeyDown(KeyCode.Q))
        {
            index = (index - 1 + Lcam.Length) % Lcam.Length;
            SetTarget(index);
        }

        // Switch to the next camera (E)
        if (Input.GetKeyDown(KeyCode.E))
        {
            index = (index + 1) % Lcam.Length;
            SetTarget(index);
        }
    }

    // Set the target position and rotation for the camera
    private void SetTarget(int targetIndex)
    {
        targetPosition = Lcam[targetIndex].transform.position;
        targetRotation = Lcam[targetIndex].transform.rotation;
    }
}