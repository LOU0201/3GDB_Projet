using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Appear : MonoBehaviour
{
    public float fadeTime = 1f;
    public CanvasGroup mainImageCanvasGroup;
    public RectTransform mainImageRectTransform;
    public List<GameObject> uiElements = new List<GameObject>();
    public Ease animationEase = Ease.OutElastic; // DOTween's easing type

    public Vector2 originalPosition;

    void Awake()
    {
        if (mainImageRectTransform != null)
        {
            originalPosition = mainImageRectTransform.anchoredPosition;
            mainImageCanvasGroup.alpha = 0f; // Ensure it's hidden initially
            gameObject.SetActive(true); // Activate this UI element
            PlayMainImageAnimation();
        }
    }

    void PlayMainImageAnimation()
    {
        // Move and fade in the main image
        mainImageRectTransform.anchoredPosition = new Vector2(originalPosition.x, originalPosition.y - 1000f);
        mainImageRectTransform.DOAnchorPos(originalPosition, fadeTime).SetEase(animationEase);
        mainImageCanvasGroup.DOFade(1, fadeTime).OnComplete(() =>
        {
            StartCoroutine(ShowUIElements());
        });
    }

    IEnumerator ShowUIElements()
    {
        foreach (var element in uiElements)
        {
            element.SetActive(true);
            element.transform.localScale = Vector3.zero;
            element.transform.DOScale(1f, fadeTime).SetEase(Ease.OutBounce);
            yield return new WaitForSeconds(0.25f);
        }
    }
}
