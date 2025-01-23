using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Pause_menu_anim : MonoBehaviour
{
    public float fadeTime = 1f;
    public CanvasGroup canvasGroup;
    public RectTransform rectTransform;
    public List<GameObject> icons = new List<GameObject>();

    private Vector2 originalPosition;
    private bool isMenuVisible = false; // Tracks whether the menu is visible

    void Start()
    {
        originalPosition = rectTransform.anchoredPosition;
        canvasGroup.alpha = 0; // Ensure the menu is hidden initially
    }

    void Update()
    {
        // Check if ESC is pressed and the menu is visible
        if (Input.GetKeyDown(KeyCode.Escape) && isMenuVisible)
        {
            FadeOut(); // Hide the menu when ESC is pressed
            isMenuVisible = false; // Update visibility state
        }
    }

    public void FadeIn()
    {
        canvasGroup.alpha = 0f;
        rectTransform.anchoredPosition = new Vector2(originalPosition.x, originalPosition.y - 1000f);
        rectTransform.DOAnchorPos(originalPosition, fadeTime, false).SetEase(Ease.OutElastic);
        canvasGroup.DOFade(1, fadeTime);

        StartCoroutine(IconAnimation());
        isMenuVisible = true; // Update visibility state
    }

    public void FadeOut()
    {
        canvasGroup.alpha = 1f;
        rectTransform.transform.localPosition = new Vector3(0f, 0f, 0f);
        rectTransform.DOAnchorPos(new Vector2(0f, -1000f), fadeTime, false).SetEase(Ease.InOutQuint);
        canvasGroup.DOFade(0, fadeTime);

        isMenuVisible = false; // Update visibility state
    }

    IEnumerator IconAnimation()
    {
        foreach (var icon in icons)
        {
            icon.transform.localScale = Vector3.zero;
        }

        foreach (var icon in icons)
        {
            icon.transform.DOScale(1f, fadeTime).SetEase(Ease.OutBounce);
            yield return new WaitForSeconds(0.25f);
        }
    }
}
