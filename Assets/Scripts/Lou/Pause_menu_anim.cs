using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause_menu_anim : MonoBehaviour
{
    public CanvasGroup background; // Background CanvasGroup to control fade
    public CanvasGroup[] icons;    // Icons CanvasGroups to control fade
    public float fadeSpeed = 1f;   // Speed of fading
    public float iconDelay = 0.1f; // Delay between fading icons

    private bool isAnimating = false; // Prevent overlapping animations
    private bool isPaused = false;    // Track pause state

    public void TogglePauseMenu()
    {
        if (isAnimating)
            return;

        if (isPaused)
        {
            StartCoroutine(FadeOut());
        }
        else
        {
            StartCoroutine(FadeIn());
        }

        isPaused = !isPaused; // Toggle the pause state
    }

    public IEnumerator FadeIn()
    {
        isAnimating = true;

        // Fade in the background
        yield return StartCoroutine(FadeCanvasGroup(background, 0f, 1f, fadeSpeed));

        // Fade in the icons, starting from the nearest
        foreach (var icon in icons)
        {
            StartCoroutine(FadeCanvasGroup(icon, 0f, 1f, fadeSpeed));
            yield return new WaitForSeconds(iconDelay);
        }

        isAnimating = false;
    }

    public IEnumerator FadeOut()
    {
        isAnimating = true;

        // Fade out the icons, starting from the farthest
        for (int i = icons.Length - 1; i >= 0; i--)
        {
            StartCoroutine(FadeCanvasGroup(icons[i], 1f, 0f, fadeSpeed));
            yield return new WaitForSeconds(iconDelay);
        }

        // Fade out the background
        yield return StartCoroutine(FadeCanvasGroup(background, 1f, 0f, fadeSpeed));

        isAnimating = false;
    }

    private IEnumerator FadeCanvasGroup(CanvasGroup canvasGroup, float startAlpha, float endAlpha, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = endAlpha; // Snap to the final alpha
    }
}
