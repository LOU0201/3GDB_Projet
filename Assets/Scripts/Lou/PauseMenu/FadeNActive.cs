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

    private void Start()
    {
        if (fadeText != null)
        {
            // Fade out the text
            fadeText.DOFade(0f, fadeDuration).SetEase(Ease.Linear);
        }

        // Start the delayed activation process
        StartCoroutine(ActivateScriptAfterFade());
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

        // Optional: Self-destruction after activating the script
        //Destroy(this);
    }
}

