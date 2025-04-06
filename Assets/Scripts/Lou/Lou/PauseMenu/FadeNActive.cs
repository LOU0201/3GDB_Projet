using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class FadeNActive : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI fadeText; // Text to fade
    [SerializeField] private float fadeDuration = 1f; // Fade duration
    [SerializeField] private MonoBehaviour targetScript; // Script to enable after fading
    [SerializeField] private float delayBeforeActivate = 1f; // Delay before activating the script
    [SerializeField] private Camera sideCamera; // Reference to the side camera
    [SerializeField] private Camera mainCamera; // Reference to the main camera
    [SerializeField] private GameObject PauseButton; // UI element to activate
    [SerializeField] private GameObject Intro; // Parent GameObject to deactivate

    private Coroutine activationCoroutine; // Reference to the coroutine

    private void Start()
    {
        // Start the fade process if the text is assigned
        if (fadeText != null)
        {
            fadeText.DOFade(0f, fadeDuration).SetEase(Ease.Linear);
        }

        // Start the delayed activation process
        activationCoroutine = StartCoroutine(ActivateScriptAfterFade());
    }

    private void Update()
    {
        // Check for left mouse click
        if (Input.GetMouseButtonDown(0))
        {
            SkipProcess();
        }
    }

    private IEnumerator ActivateScriptAfterFade()
    {
        // Wait for the fade duration plus the additional delay
        yield return new WaitForSeconds(fadeDuration + delayBeforeActivate);

        // Activate the target script
        if (targetScript != null)
        {
            targetScript.enabled = true;
        }
    }

    private void SkipProcess()
    {
        // Stop the ongoing activation coroutine
        if (activationCoroutine != null)
        {
            StopCoroutine(activationCoroutine);
        }

        // Immediately reset fade (if ongoing) and make text fully visible
        if (fadeText != null)
        {
            fadeText.DOKill(); // Stops any ongoing DOTween process
            fadeText.alpha = 1f; // Reset text visibility
        }

        sideCamera.gameObject.SetActive(false);

        mainCamera.gameObject.SetActive(true);

        // Activate pause menu     
            PauseButton.SetActive(true);       
        // Deactivate intro     
            Intro.SetActive(false);
        
    }
}

