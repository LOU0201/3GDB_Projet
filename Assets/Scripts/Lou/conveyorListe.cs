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

    void Start()
    {
        InitializeConveyor();
        InitializeArrow();
    }

    // Initializes the conveyor with all elements from the ListeTom
    void InitializeConveyor()
    {
        if (listeTom == null || listeTom.liste.Length == 0)
        {
            Debug.LogError("ListeTom is not assigned or empty!");
            return;
        }

        // Get conveyor width and element spacing
        float conveyorWidth = conveyorBelt.rect.width;
        float elementWidth = conveyorWidth / listeTom.liste.Length;

        // Create and position initial elements from left to right
        for (int i = 0; i < listeTom.liste.Length; i++)
        {
            GameObject element = Instantiate(prefabElement, conveyorBelt);

            // Position from left to right
            float posX = -conveyorWidth / 2 + elementWidth / 2 + i * elementWidth;
            element.transform.localPosition = new Vector3(posX, 0, 0);

            // Update element sprite based on the item from ListeTom
            string item = listeTom.liste[i];
            UpdateElementSprite(element.GetComponent<Image>(), item);
        }

        // Position the arrow at the first element
        UpdateArrowPosition();
    }

    // Initializes the arrow indicator above the first element
    void InitializeArrow()
    {
        if (arrowIndicator == null)
        {
            Debug.LogError("Arrow Indicator is not assigned!");
            return;
        }

        // Make the arrow a child of the conveyor
        arrowIndicator.transform.SetParent(conveyorBelt);

        // Ensure the arrow is properly anchored and positioned
        RectTransform arrowRect = arrowIndicator.GetComponent<RectTransform>();
        arrowRect.anchorMin = new Vector2(0.5f, 1); // Anchor to the top center
        arrowRect.anchorMax = new Vector2(0.5f, 1);
        arrowRect.pivot = new Vector2(0.5f, 0);    // Pivot bottom-center
        UpdateArrowPosition();
    }

    // Updates the conveyor display (called when the player moves or the list is updated)
    public void UpdateConveyor()
    {
        if (listeTom == null || listeTom.liste.Length == 0)
        {
            Debug.LogError("ListeTom is not assigned or empty!");
            return;
        }

        // Get conveyor width and element spacing
        float conveyorWidth = conveyorBelt.rect.width;
        float elementWidth = conveyorWidth / listeTom.liste.Length;

        // Loop through all elements
        for (int i = 0; i < listeTom.liste.Length; i++)
        {
            // Find the element's position (starting from left to right)
            Transform elementTransform = conveyorBelt.GetChild(i);
            float posX = -conveyorWidth / 2 + elementWidth / 2 + i * elementWidth;
            elementTransform.localPosition = new Vector3(posX, 0, 0);

            // Update the sprite of the element based on the item from ListeTom
            string item = listeTom.liste[i];
            UpdateElementSprite(elementTransform.GetComponent<Image>(), item);
        }

        // Update the arrow position to point to the current element
        UpdateArrowPosition();
    }

    // Updates the arrow's position to align with the current active element
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

        // Get the index of the current active element
        int currentIndex = listeTom.currentIndex;

        // Find the current element in the conveyor
        Transform currentElement = conveyorBelt.GetChild(currentIndex);

        // Position the arrow above the current element
        Vector3 arrowPosition = currentElement.localPosition + new Vector3(0, 50, 0); // Adjust Y-offset as needed
        arrowIndicator.GetComponent<RectTransform>().localPosition = arrowPosition;
    }

    // Updates the sprite of a conveyor element based on the list item
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
