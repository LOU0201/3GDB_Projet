using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class conveyorListe : MonoBehaviour
{
    public GameObject prefabElement; // Prefab for conveyor elements
    public RectTransform conveyorBelt; // UI container for the conveyor
    public ListeTom listeTom; // Reference to ListeTom
    public GameObject arrowIndicator; // Arrow indicator GameObject

    private Vector3 normalScale = Vector3.one; // Normal scale for elements
    private Vector3 highlightScale = Vector3.one * 1.2f; // Scaled-up size for highlighted elements

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

        for (int i = 0; i < listeTom.liste.Length; i++)
        {
            GameObject element = Instantiate(prefabElement, conveyorBelt);
            float posX = -conveyorWidth / 2 + elementWidth / 2 + i * elementWidth;
            element.transform.localPosition = new Vector3(posX, 0, 0);
            string item = listeTom.liste[i];
            UpdateElementSprite(element.GetComponent<Image>(), item);
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

        arrowIndicator.transform.SetParent(conveyorBelt);
        RectTransform arrowRect = arrowIndicator.GetComponent<RectTransform>();
        arrowRect.anchorMin = new Vector2(0.5f, 1);
        arrowRect.anchorMax = new Vector2(0.5f, 1);
        arrowRect.pivot = new Vector2(0.5f, 0);
        UpdateArrowPosition();
    }

    public void UpdateConveyor()
    {
        if (listeTom == null || listeTom.liste.Length == 0)
        {
            Debug.LogError("ListeTom is not assigned or empty!");
            return;
        }

        float conveyorWidth = conveyorBelt.rect.width;
        float elementWidth = conveyorWidth / listeTom.liste.Length;

        for (int i = 0; i < listeTom.liste.Length; i++)
        {
            Transform elementTransform = conveyorBelt.GetChild(i);
            float posX = -conveyorWidth / 2 + elementWidth / 2 + i * elementWidth;
            elementTransform.localPosition = new Vector3(posX, 0, 0);

            string item = listeTom.liste[i];
            UpdateElementSprite(elementTransform.GetComponent<Image>(), item);
        }

        UpdateHighlightedElements();
        UpdateArrowPosition();
    }

    private void UpdateHighlightedElements()
    {
        for (int i = 0; i < listeTom.liste.Length; i++)
        {
            Transform elementTransform = conveyorBelt.GetChild(i);
            if (i == listeTom.currentIndex) // Current event
            {
                elementTransform.localScale = highlightScale;
            }
            else if (i > listeTom.currentIndex && i <= listeTom.currentIndex + 2) // Next two events
            {
                elementTransform.localScale = highlightScale * 0.9f; // Slightly smaller than current
            }
            else
            {
                elementTransform.localScale = normalScale; // Reset others to default
            }
        }
    }

    private void UpdateArrowPosition()
    {
        if (arrowIndicator == null)
        {
            Debug.LogError("Arrow Indicator is not assigned!");
            return;
        }

        if (listeTom.liste.Length == 0)
        {
            Debug.LogWarning("ListeTom is empty, cannot update arrow position.");
            return;
        }

        int currentIndex = listeTom.currentIndex;
        Transform currentElement = conveyorBelt.GetChild(currentIndex);
        Vector3 arrowPosition = currentElement.localPosition + new Vector3(0, 50, 0); // Adjust Y-offset
        arrowIndicator.GetComponent<RectTransform>().localPosition = arrowPosition;
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
