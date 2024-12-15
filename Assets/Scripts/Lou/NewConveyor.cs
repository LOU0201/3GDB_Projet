using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewConveyor : MonoBehaviour
{
    public GameObject prefabElement;
    public RectTransform conveyorBelt;
    public ListeTom listeTom;
    public GameObject arrowIndicator;

    public Vector3 normalScale = Vector3.one;
    public Vector3 highlightScale = Vector3.one * 1.2f;

    private List<RectTransform> elementRectTransforms = new List<RectTransform>();

    void Start()
    {
        InitializeConveyor();
        InitializeArrow();
    }

    void InitializeConveyor()
    {
        if (listeTom == null || listeTom.liste.Length == 0)
        {
            Debug.LogError("ListeTom is not assigned or empty!");
            return;
        }

        float conveyorWidth = conveyorBelt.rect.width;
        float elementWidth = conveyorWidth / listeTom.liste.Length;

        elementRectTransforms.Clear(); // Clear the list before populating

        for (int i = 0; i < listeTom.liste.Length; i++)
        {
            GameObject element = Instantiate(prefabElement, conveyorBelt);
            float posX = -conveyorWidth / 2 + elementWidth / 2 + i * elementWidth;
            element.GetComponent<RectTransform>().anchoredPosition = new Vector2(posX, 0);

            string item = listeTom.liste[i];
            UpdateElementSprite(element.GetComponent<Image>(), item);

            RectTransform elementRect = element.GetComponent<RectTransform>();
            if (elementRect == null)
            {
                Debug.LogError("Element does not have a RectTransform!");
                return;
            }
            elementRectTransforms.Add(elementRect);
        }

        UpdateHighlightedElements();
        UpdateArrowPosition();
    }

    void InitializeArrow()
    {
        if (arrowIndicator == null)
        {
            Debug.LogError("Arrow Indicator is not assigned!");
            return;
        }

        arrowIndicator.transform.SetParent(conveyorBelt, false);
        RectTransform arrowRect = arrowIndicator.GetComponent<RectTransform>();

        arrowRect.anchorMin = new Vector2(0.5f, 1);
        arrowRect.anchorMax = new Vector2(0.5f, 1);
        arrowRect.pivot = new Vector2(0.5f, 0);
        arrowRect.anchoredPosition = new Vector2(0, 30);

        UpdateArrowPosition();
    }

    public void UpdateConveyor()
    {
        if (listeTom == null || listeTom.liste.Length == 0)
        {
            Debug.LogError("ListeTom is not assigned or empty!");
            return;
        }
        if (elementRectTransforms.Count != listeTom.liste.Length)
        {
            InitializeConveyor();
            Debug.LogWarning("Reinitializing Conveyor");
        }
        UpdateHighlightedElements();
        UpdateArrowPosition();
    }

    void UpdateHighlightedElements()
    {
        if (elementRectTransforms.Count != listeTom.liste.Length)
        {
            Debug.LogError("RectTransform list count does not match list length!");
            return;
        }

        for (int i = 0; i < elementRectTransforms.Count; i++)
        {
            RectTransform elementRect = elementRectTransforms[i];

            if (elementRect == null)
            {
                Debug.LogError($"RectTransform at index {i} is null!");
                continue;
            }

            if (i == listeTom.currentIndex)
            {
                elementRect.localScale = highlightScale;
                Debug.Log($"Highlighting element at index {i}, Scale: {elementRect.localScale}");
            }
            else
            {
                elementRect.localScale = normalScale;
            }
        }
    }

    void UpdateArrowPosition()
    {
        if (arrowIndicator == null)
        {
            Debug.LogError("Arrow Indicator is not assigned!");
            return;
        }

        if (listeTom == null || listeTom.liste.Length == 0)
        {
            Debug.LogWarning("ListeTom is null or empty, cannot update arrow position.");
            return;
        }

        if (listeTom.currentIndex < 0 || listeTom.currentIndex >= listeTom.liste.Length)
        {
            Debug.LogError($"currentIndex is out of range: {listeTom.currentIndex}");
            return;
        }

        if (elementRectTransforms.Count <= listeTom.currentIndex)
        {
            Debug.LogError($"RectTransform list has fewer elements than currentIndex: {elementRectTransforms.Count} <= {listeTom.currentIndex}");
            return;
        }

        RectTransform currentElementRect = elementRectTransforms[listeTom.currentIndex];
        if (currentElementRect == null)
        {
            Debug.LogError($"Current element RectTransform at index {listeTom.currentIndex} is null!");
            return;
        }

        Vector3 arrowPosition = currentElementRect.anchoredPosition + new Vector2(0, 70);
        arrowIndicator.GetComponent<RectTransform>().anchoredPosition = arrowPosition;
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
