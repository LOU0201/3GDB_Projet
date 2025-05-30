using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Flyin : MonoBehaviour
{
    public RectTransform[] buttons; 
    public float animationSpeed = 500f; // Speed of the fly-in animation
    public float delayBetweenButtons = 0.2f; // Delay between each button's animation
    public float offscreenDistance = 500f; // How far offscreen the buttons start
    public bool skipAnimation = false; // To check if the player skips the animation
    public float animationAcceleration = 1f;
    public GameObject logo;

    private Vector2[] originalPositions; // Store the buttons' final positions
    private Coroutine animationCoroutine;
    private Animator logoAnimator;

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
        logoAnimator = logo.GetComponent<Animator>();

    }

    private void Update()
    {
        // If any key is pressed or mouse button clicked, skip the animation
        if (!skipAnimation && (Input.anyKeyDown || Input.GetMouseButtonDown(0)))
        {
            skipAnimation = true;
            logoAnimator.speed = 10f;
            // If the animation is running, stop it
            animationAcceleration = 10f;
            StopCoroutine(animationCoroutine);
            // Set all buttons to their final positions
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
            elapsedTime += Time.deltaTime * animationAcceleration;
            yield return null;
        }

        button.anchoredPosition = targetPosition; // Snap to final position
    }
}
