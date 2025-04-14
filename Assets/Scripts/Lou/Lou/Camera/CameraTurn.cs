using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class CameraTurn : MonoBehaviour
{
    [SerializeField] private Transform sideCameraTransform; 
    [SerializeField] private Transform playerTransform; 
    [SerializeField] private GameObject mainCamera; 
    [SerializeField] private GameObject uiElement; 
    public float rotationSpeed = 50f; // Speed of the camera rotation

    private bool isRotating = false;
    public float targetYPosition = 19.32f; // Target Y position for the side camera
    public GameObject _joueur;
    public GameObject _sprite;

    void Start()
    {
        if (mainCamera != null)
            mainCamera.SetActive(false); // Ensure the main camera is deactivated initially

        if (uiElement != null)
            uiElement.SetActive(false); // Ensure the UI element is deactivated initially
    }

    void Update()
    {
        if (!isRotating)
        {
            StartCoroutine(MoveAndRotateCamera());
        }
    }

    private IEnumerator MoveAndRotateCamera()
    {
        isRotating = true;

        // Move the camera to the target Y position while rotating
        while (Mathf.Abs(sideCameraTransform.position.y - targetYPosition) > 0.01f) // Ensure it stops at the target position
        {
            float rotationStep = rotationSpeed * Time.deltaTime;

            // Rotate around the player on the Y-axis
            sideCameraTransform.RotateAround(playerTransform.position, Vector3.up, rotationStep);

            // Move downward towards the target Y position
            Vector3 currentPosition = sideCameraTransform.position;
            currentPosition.y = Mathf.MoveTowards(currentPosition.y, targetYPosition, rotationStep * 0.1f); // Adjust movement speed as needed
            sideCameraTransform.position = currentPosition;

            yield return null;
        }

        // Ensure the Y position is exactly at the target to prevent overshooting
        Vector3 finalPosition = sideCameraTransform.position;
        finalPosition.y = targetYPosition;
        sideCameraTransform.position = finalPosition;

        // Complete one full rotation after reaching the target position
        float totalRotation = 0f;
        while (totalRotation < 360f)
        {
            float rotationStep = rotationSpeed * Time.deltaTime;
            totalRotation += rotationStep;
            sideCameraTransform.RotateAround(playerTransform.position, Vector3.up, rotationStep);
            yield return null;
        }

        // Reset the Y rotation to 0 to avoid abrupt changes
        Quaternion resetRotation = sideCameraTransform.rotation;
        resetRotation.eulerAngles = new Vector3(resetRotation.eulerAngles.x, 0f, resetRotation.eulerAngles.z);
        sideCameraTransform.rotation = resetRotation;

        // Activate the main camera and UI, and deactivate this object
        if (mainCamera != null)
        {
            mainCamera.SetActive(true);
        }

        if (uiElement != null)
        {
            uiElement.SetActive(true);
        }

        gameObject.SetActive(false);

        isRotating = false;
        _joueur.SetActive(true);
        _sprite.SetActive(true);
    }
}
