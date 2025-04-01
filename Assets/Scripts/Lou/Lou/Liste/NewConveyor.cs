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
        ResetElementsScale();
    }

    void InitializeConveyor()
    {
       
        float conveyorWidth = conveyorBelt.rect.width;
        float elementWidth = conveyorWidth / listeTom.liste.Length;

        elementRectTransforms.Clear();

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

            elementRectTransforms.Add(elementRect);
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

            if (elementRect == null)
            {
               
                continue;
            }

            if (i == listeTom.currentIndex)
            {
                elementRect.localScale = highlightScale;
               
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
