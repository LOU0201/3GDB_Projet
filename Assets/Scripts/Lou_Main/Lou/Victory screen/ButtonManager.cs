using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public Appear uiAnimationController;
    public CanvasGroup fadeCanvasGroup; // Black fade overlay
    public float fadeToBlackTime = 1f;

    public Button retryButton;
    public Button returnButton;
    public Button nextButton;

    void Start()
    {
        fadeCanvasGroup.alpha = 0f; // Ensure the black screen is invisible at the start

        // Assign button listeners
        retryButton.onClick.AddListener(() => StartCoroutine(PlayExitAnimation(() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex))));
        returnButton.onClick.AddListener(() => StartCoroutine(PlayExitAnimation(() => SceneManager.LoadScene("HUB"))));
        nextButton.onClick.AddListener(() => StartCoroutine(PlayExitAnimation(() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1))));
    }

    IEnumerator PlayExitAnimation(System.Action onComplete)
    {
        // Disable button interactions to prevent multiple clicks
        SetButtonsInteractable(false);

        yield return StartCoroutine(HideUIElements()); // Step 1: Fade out UI elements
        yield return StartCoroutine(HideMainImageAnimation()); // Step 2: Play main image animation out
        yield return StartCoroutine(FadeToBlack()); // Step 3: Fade to black

        onComplete?.Invoke(); // Step 4: Execute the selected function
    }

    IEnumerator HideUIElements()
    {
        for (int i = uiAnimationController.uiElements.Count - 1; i >= 0; i--)
        {
            var element = uiAnimationController.uiElements[i];
            element.transform.DOScale(0, uiAnimationController.fadeTime).SetEase(Ease.InBack);
            yield return new WaitForSeconds(0.2f);
        }
    }

    IEnumerator HideMainImageAnimation()
    {
        uiAnimationController.mainImageRectTransform.DOAnchorPos(
            new Vector2(uiAnimationController.originalPosition.x, uiAnimationController.originalPosition.y - 1000f),
            uiAnimationController.fadeTime
        ).SetEase(Ease.InOutQuint);

        uiAnimationController.mainImageCanvasGroup.DOFade(0, uiAnimationController.fadeTime);
        yield return new WaitForSeconds(uiAnimationController.fadeTime);
    }

    IEnumerator FadeToBlack()
    {
        fadeCanvasGroup.DOFade(1, fadeToBlackTime);
        yield return new WaitForSeconds(fadeToBlackTime);
    }

    void SetButtonsInteractable(bool interactable)
    {
        retryButton.interactable = interactable;
        returnButton.interactable = interactable;
        nextButton.interactable = interactable;
    }
}
