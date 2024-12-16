using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CameraTurn : MonoBehaviour
{
    public Transform sideCameraTransform; // Side camera to perform the rotation
    public Transform playerTransform; // The player to rotate around
    public GameObject mainCamera; // Main camera to activate after the side camera
    public GameObject uiElement; // UI element to activate after rotation
    public CanvasGroup panelToFade; // Panel to fade out
    public TextMeshProUGUI textToFade; // TMP text to fade out
    public float rotationSpeed = 50f; // Speed of the camera rotation
    public float fadeDuration = 2f; // Duration of the fade effect

    private Quaternion originalRotation; // Original rotation of the side camera
    private bool isRotating = false;

    void Start()
    {
        if (sideCameraTransform == null)
            sideCameraTransform = Camera.main.transform;

        originalRotation = sideCameraTransform.rotation;

        if (mainCamera != null)
            mainCamera.SetActive(false); // Ensure the main camera is deactivated initially

        if (uiElement != null)
            uiElement.SetActive(false); // Ensure the UI element is initially inactive

        if (panelToFade != null)
            panelToFade.alpha = 1f; // Ensure full opacity initially

        if (textToFade != null)
        {
            Color color = textToFade.color;
            color.a = 1f; // Ensure text is fully visible initially
            textToFade.color = color;
        }
    }

    void Update()
    {
        if (panelToFade.alpha > 0f || (textToFade != null && textToFade.color.a > 0f))
        {
            // Fade out the panel
            if (panelToFade != null)
            {
                panelToFade.alpha -= Time.deltaTime / fadeDuration;

                if (panelToFade.alpha <= 0f)
                    panelToFade.gameObject.SetActive(false);
            }

            // Fade out the text
            if (textToFade != null)
            {
                Color color = textToFade.color;
                color.a -= Time.deltaTime / fadeDuration;
                textToFade.color = color;

                if (color.a <= 0f)
                    textToFade.gameObject.SetActive(false);
            }

            // Start the rotation only after both fades are complete
            if (panelToFade != null && panelToFade.alpha <= 0f &&
                textToFade != null && textToFade.color.a <= 0f &&
                !isRotating)
            {
                StartCoroutine(RotateAroundPlayer());
            }
        }
    }

    IEnumerator RotateAroundPlayer()
    {
        isRotating = true;

        float totalRotation = 0f;

        while (totalRotation < 360f)
        {
            float step = rotationSpeed * Time.deltaTime;

            // Rotate the camera around the player on the Y-axis
            sideCameraTransform.RotateAround(playerTransform.position, Vector3.up, step);

            totalRotation += step;

            yield return null;
        }

        // Return the camera to its original position and rotation
        sideCameraTransform.rotation = originalRotation;

        // Activate the main camera and deactivate the side camera
        if (mainCamera != null)
        {
            mainCamera.SetActive(true);
        }
        gameObject.SetActive(false);

        // Activate the UI element
        if (uiElement != null)
        {
            uiElement.SetActive(true);
        }

        isRotating = false;
    }
}
