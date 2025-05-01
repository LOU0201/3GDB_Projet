using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DisplayAnimation : MonoBehaviour
{
    [Header("UI Elements")]
    public CanvasGroup[] uiElementsToShow;

    [Header("Animation Settings")]
    public float delayBetweenElements = 0.2f;
    public float fadeInDuration = 0.5f;
    public Ease fadeEase = Ease.OutQuad;

    void Awake()
    {
        InitializeUI();
    }

    public void PlayEnterAnimation()
    {
        AnimateElementsInSequence();
    }

    private void InitializeUI()
    {
        foreach (var cg in uiElementsToShow)
        {
            if (cg != null)
            {
                cg.alpha = 0;
                cg.interactable = false;
                cg.blocksRaycasts = false;
            }
        }
    }

    private void AnimateElementsInSequence()
    {
        float delay = 0f;

        foreach (var cg in uiElementsToShow)
        {
            if (cg == null) continue;

            // Create and sequence the fade animation
            cg.DOFade(1f, fadeInDuration)
                .SetDelay(delay)
                .SetEase(fadeEase)
                .OnComplete(() => {
                    cg.interactable = true;
                    cg.blocksRaycasts = true;
                });

            delay += delayBetweenElements;
        }
    }
}