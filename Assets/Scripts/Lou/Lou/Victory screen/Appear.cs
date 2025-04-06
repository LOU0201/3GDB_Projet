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
    public Ease animationEase = Ease.OutElastic;
    public StarRating starRatingSystem; 

    public Vector2 originalPosition;

    void Awake()
    {
        if (mainImageRectTransform != null)
        {
            originalPosition = mainImageRectTransform.anchoredPosition;
            mainImageCanvasGroup.alpha = 0f;
            gameObject.SetActive(true);
            PlayMainImageAnimation();
        }
    }

    void PlayMainImageAnimation()
    {
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

        // After all UI elements animate, call the Star Rating System
      
            starRatingSystem.UpdateStarRating();
        
    }
}
