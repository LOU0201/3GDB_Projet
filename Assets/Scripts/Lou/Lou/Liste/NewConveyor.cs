using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewConveyor : MonoBehaviour
{
    public GameObject prefabElement;
    public RectTransform conveyorBelt;
    public ListeTom listeTom;

    [Header("Scaling Settings")]
    public Vector3 normalScale = Vector3.one;
    public Vector3 highlightScale = Vector3.one * 1.2f;

    [Header("Bubble Settings")]
    public Sprite bubbleSprite;
    public Sprite bubbleBurstSprite;
    public float bubbleAnimationDuration = 0.3f;
    public Vector3 bubbleScale = Vector3.one; // Added X,Y,Z scaling control

    private List<RectTransform> elementRectTransforms = new List<RectTransform>();
    private List<Image> bubbleImages = new List<Image>();
    private Coroutine bubbleBurstCoroutine;

    void Start()
    {
        InitializeConveyor();
        ResetElementsScale();
    }

    void InitializeConveyor()
    {
        float conveyorWidth = conveyorBelt.rect.width;
        float elementWidth = conveyorWidth / listeTom.liste.Length;

        elementRectTransforms.Clear();
        bubbleImages.Clear();

        for (int i = 0; i < listeTom.liste.Length; i++)
        {
            GameObject element = Instantiate(prefabElement, conveyorBelt);
            RectTransform elementRect = element.GetComponent<RectTransform>();

            elementRect.anchorMin = new Vector2(0.5f, 0.5f);
            elementRect.anchorMax = new Vector2(0.5f, 0.5f);
            elementRect.pivot = new Vector2(0.5f, 0.5f);

            float posX = -conveyorWidth / 2 + elementWidth / 2 + i * elementWidth;
            elementRect.anchoredPosition = new Vector2(posX, 0);

            string item = listeTom.liste[i];
            UpdateElementSprite(element.GetComponent<Image>(), item);

            // Create bubble overlay
            GameObject bubbleObj = new GameObject("Bubble");
            bubbleObj.transform.SetParent(element.transform, false);
            Image bubbleImage = bubbleObj.AddComponent<Image>();
            bubbleImage.sprite = bubbleSprite;
            bubbleImage.rectTransform.anchorMin = Vector2.zero;
            bubbleImage.rectTransform.anchorMax = Vector2.one;
            bubbleImage.rectTransform.offsetMin = Vector2.zero;
            bubbleImage.rectTransform.offsetMax = Vector2.zero;
            bubbleImage.rectTransform.localScale = bubbleScale; // Apply custom scaling

            elementRectTransforms.Add(elementRect);
            bubbleImages.Add(bubbleImage);
        }

        UpdateHighlightedElements();
    }

    public void UpdateConveyor()
    {
        if (elementRectTransforms.Count != listeTom.liste.Length)
        {
            InitializeConveyor();
        }

        UpdateHighlightedElements();
    }

    public void ResetElementsScale()
    {
        foreach (var elementRect in elementRectTransforms)
        {
            if (elementRect != null)
            {
                elementRect.localScale = normalScale;
            }
        }
    }

    void UpdateHighlightedElements()
    {
        for (int i = 0; i < elementRectTransforms.Count; i++)
        {
            RectTransform elementRect = elementRectTransforms[i];
            Image bubbleImage = bubbleImages[i];

            if (elementRect == null || bubbleImage == null)
            {
                continue;
            }

            if (i == listeTom.currentIndex)
            {
                // Highlighted element - burst the bubble
                elementRect.localScale = highlightScale;
                if (bubbleBurstCoroutine != null)
                {
                    StopCoroutine(bubbleBurstCoroutine);
                }
                bubbleBurstCoroutine = StartCoroutine(BurstBubble(bubbleImage));
            }
            else
            {
                // Unhighlighted element - show bubble
                elementRect.localScale = normalScale;
                bubbleImage.sprite = bubbleSprite;
                bubbleImage.enabled = true;
                bubbleImage.rectTransform.localScale = bubbleScale; // Ensure scale is maintained
            }
        }
    }

    IEnumerator BurstBubble(Image bubbleImage)
    {
        if (bubbleImage != null)
        {
            // Show burst sprite
            bubbleImage.sprite = bubbleBurstSprite;
            bubbleImage.rectTransform.localScale = bubbleScale; // Maintain scale during burst

            // Wait for a short duration
            yield return new WaitForSeconds(bubbleAnimationDuration);

            // Disable the bubble
            if (bubbleImage != null)
            {
                bubbleImage.enabled = false;
            }
        }
    }

    private void UpdateElementSprite(Image image, string item)
    {
        if (item == "cube")
        {
            image.sprite = listeTom.cubeSprite;
        }
        else if (item == "trou")
        {
            image.sprite = listeTom.trouSprite;
        }
        else
        {
            image.sprite = listeTom.rienSprite;
        }
    }
}
