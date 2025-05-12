using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections.Generic;

public class DisplayAnimation : MonoBehaviour
{
    [Header("UI Elements")]
    public RectTransform backgroundPanel;
    public CanvasGroup[] uiElementsToShow;

    [Header("Animation Settings")]
    public float backgroundAnimDuration = 0.3f;
    public float elementsFadeInDuration = 0.2f;
    public float delayBetweenElements = 0.1f;
    public Ease backgroundEase = Ease.OutBack;
    public Ease fadeEase = Ease.Linear;

    private Vector2 originalBackgroundSize;
    private Sequence showSequence;
    private Sequence hideSequence;

    void Awake()
    {
        // Store original background size
        originalBackgroundSize = backgroundPanel.sizeDelta;

        // Initialize sequences
        InitializeShowSequence();
        InitializeHideSequence();
    }

    void InitializeShowSequence()
    {
        showSequence = DOTween.Sequence();
        showSequence.SetAutoKill(false);
        showSequence.Pause();

        // Reset background scale to zero
        backgroundPanel.sizeDelta = new Vector2(backgroundPanel.sizeDelta.x, 0);

        // Reset alpha for all elements
        foreach (var element in uiElementsToShow)
        {
            if (element != null) element.alpha = 0;
        }

        // 1. Scale background up
        showSequence.Append(
            DOTween.To(
                () => backgroundPanel.sizeDelta.y,
                y => backgroundPanel.sizeDelta = new Vector2(backgroundPanel.sizeDelta.x, y),
                originalBackgroundSize.y,
                backgroundAnimDuration)
            .SetEase(backgroundEase)
        );

        // 2. Fade in elements one by one
        foreach (var element in uiElementsToShow)
        {
            if (element != null)
            {
                showSequence.Append(
                    element.DOFade(1, elementsFadeInDuration)
                        .SetEase(fadeEase)
                );
                showSequence.AppendInterval(delayBetweenElements);
            }
        }
    }

    void InitializeHideSequence()
    {
        hideSequence = DOTween.Sequence();
        hideSequence.SetAutoKill(false);
        hideSequence.Pause();

        // 1. Fade out all elements
        for (int i = uiElementsToShow.Length - 1; i >= 0; i--)
        {
            var element = uiElementsToShow[i];
            if (element != null)
            {
                hideSequence.Append(
                    element.DOFade(0, elementsFadeInDuration)
                        .SetEase(fadeEase)
                );
                hideSequence.AppendInterval(delayBetweenElements);
            }
        }

        // 2. Scale background down
        hideSequence.Append(
            DOTween.To(
                () => backgroundPanel.sizeDelta.y,
                y => backgroundPanel.sizeDelta = new Vector2(backgroundPanel.sizeDelta.x, y),
                0,
                backgroundAnimDuration)
            .SetEase(backgroundEase)
        );

        // 3. Deactivate game object when complete
        hideSequence.OnComplete(() => gameObject.SetActive(false));
    }

    public void PlayEnterAnimation()
    {
        if (hideSequence != null && hideSequence.IsPlaying())
            hideSequence.Pause();

        gameObject.SetActive(true);
        showSequence.Restart();
    }

    public void PlayExitAnimation()
    {
        if (showSequence != null && showSequence.IsPlaying())
            showSequence.Pause();

        hideSequence.Restart();
    }

    void OnDestroy()
    {
        // Clean up tweens
        showSequence?.Kill();
        hideSequence?.Kill();
    }
}