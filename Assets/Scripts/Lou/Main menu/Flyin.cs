using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flyin : MonoBehaviour
{
    public RectTransform[] buttons; // Assign your buttons here
    public float animationSpeed = 500f; // Speed of the fly-in animation
    public float delayBetweenButtons = 0.2f; // Delay between each button's animation
    public float offscreenDistance = 500f; // How far offscreen the buttons start
    public bool skipAnimation = false; // To check if the player skips the animation

    private Vector2[] originalPositions; // To store the buttons' final positions
    private Coroutine animationCoroutine;

    private void Start()
    {
        // Save the original positions of the buttons
        originalPositions = new Vector2[buttons.Length];
        for (int i = 0; i < buttons.Length; i++)
        {
            originalPositions[i] = buttons[i].anchoredPosition;

            // Randomize starting position (left or right)
            float randomX = Random.value > 0.5f ? -offscreenDistance : offscreenDistance;
            buttons[i].anchoredPosition = new Vector2(randomX, buttons[i].anchoredPosition.y);
        }

        // Start the fly-in animation
        animationCoroutine = StartCoroutine(AnimateButtons());
    }

    private void Update()
    {
        // If any key is pressed or mouse button clicked, skip the animation
        if (!skipAnimation && (Input.anyKeyDown || Input.GetMouseButtonDown(0)))
        {
            skipAnimation = true;

            // If the animation is running, stop it
            if (animationCoroutine != null)
            {
                StopCoroutine(animationCoroutine);
            }

            // Instantly set all buttons to their final positions
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].anchoredPosition = originalPositions[i];
            }
        }
    }

    private IEnumerator AnimateButtons()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            if (skipAnimation) yield break;

            StartCoroutine(MoveButton(buttons[i], originalPositions[i]));
            yield return new WaitForSeconds(delayBetweenButtons); // Wait before animating the next button
        }
    }

    private IEnumerator MoveButton(RectTransform button, Vector2 targetPosition)
    {
        Vector2 startPosition = button.anchoredPosition;
        float distance = Vector2.Distance(startPosition, targetPosition);
        float elapsedTime = 0f;

        while (elapsedTime < distance / animationSpeed)
        {
            if (skipAnimation) yield break;

            button.anchoredPosition = Vector2.Lerp(startPosition, targetPosition, elapsedTime / (distance / animationSpeed));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        button.anchoredPosition = targetPosition; // Snap to final position
    }
}
