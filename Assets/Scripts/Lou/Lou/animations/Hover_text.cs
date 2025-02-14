using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using TMPro;

public class Hover_text : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI legendText; // Reference to the TextMeshPro object

    // Called when the pointer enters the UI element
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (legendText != null)
        {
            legendText.gameObject.SetActive(true); // Show the text
        }
    }

    // Called when the pointer exits the UI element
    public void OnPointerExit(PointerEventData eventData)
    {
        if (legendText != null)
        {
            legendText.gameObject.SetActive(false); // Hide the text
        }
    }
}
