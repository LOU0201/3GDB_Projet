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

    private List<RectTransform> elementRectTransforms = new List<RectTransform>();

    void Start()
    {
        InitializeConveyor();
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

        elementRectTransforms.Clear();

        for (int i = 0; i < listeTom.liste.Length; i++)
        {
            GameObject element = Instantiate(prefabElement, conveyorBelt);
            RectTransform elementRect = element.GetComponent<RectTransform>();

            if (elementRect == null)
            {
                Debug.LogError("Element does not have a RectTransform!");
                return;
            }

            // Ensure elements are centered within their allocated space
            elementRect.anchorMin = new Vector2(0.5f, 0.5f);
            elementRect.anchorMax = new Vector2(0.5f, 0.5f);
            elementRect.pivot = new Vector2(0.5f, 0.5f);

            float posX = -conveyorWidth / 2 + elementWidth / 2 + i * elementWidth;
            elementRect.anchoredPosition = new Vector2(posX, 0);

            string item = listeTom.liste[i];
            UpdateElementSprite(element.GetComponent<Image>(), item);

            elementRectTransforms.Add(elementRect);
        }

        UpdateHighlightedElements();
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
