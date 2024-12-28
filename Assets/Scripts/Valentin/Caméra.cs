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
    public Transform target;
    private float fixedRotationX;    // Fixed X rotation

    void Start()
    {
        // Store the initial X rotation to keep it fixed
        fixedRotationX = cam.transform.rotation.eulerAngles.x;

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
        // Smoothly move the camera towards the target position
        cam.transform.position = Vector3.Lerp(cam.transform.position, targetPosition, Time.deltaTime * moveSpeed);

        // Adjust the target rotation to keep the X axis fixed
        Vector3 targetEulerAngles = targetRotation.eulerAngles;
        targetEulerAngles.x = fixedRotationX; // Preserve the original X rotation
        Quaternion adjustedRotation = Quaternion.Euler(targetEulerAngles);

        // Smoothly rotate the camera towards the adjusted rotation
        cam.transform.rotation = Quaternion.Slerp(cam.transform.rotation, adjustedRotation, Time.deltaTime * rotateSpeed);

        // Switch to the previous camera (E)
        if (Input.GetKeyDown(KeyCode.E))
        {
            index = (index - 1 + Lcam.Length) % Lcam.Length;
            SetTarget(index);
            FMODUnity.RuntimeManager.PlayOneShot("event:/V1/System/movecam");
        }

        // Switch to the next camera (Q)
        if (Input.GetKeyDown(KeyCode.Q))
        {
            index = (index + 1) % Lcam.Length;
            SetTarget(index);
            FMODUnity.RuntimeManager.PlayOneShot("event:/V1/System/movecam");
        }
    }

    // Set the target position and rotation for the camera
    private void SetTarget(int targetIndex)
    {
        targetPosition = Lcam[targetIndex].transform.position;
        targetRotation = Lcam[targetIndex].transform.rotation;
    }
}