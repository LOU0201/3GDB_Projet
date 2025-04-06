using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
public class Popup : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI popUpTextPrefab; // Reference to the TMP text prefab
    [SerializeField] private Transform goalTransform; // Transform of the goal
    [SerializeField] private Vector3 offset = new Vector3(0, 2f, 0); // Offset above the goal
    [SerializeField] private float popUpDuration = 1f; // Duration for the pop-up to disappear

    private TextMeshProUGUI activePopUpText; // Active pop-up text instance

    private void Start()
    {
        // Instantiate the pop-up text prefab at the start and disable it
        activePopUpText = Instantiate(popUpTextPrefab, goalTransform.position + offset, Quaternion.identity, goalTransform);
        activePopUpText.gameObject.SetActive(false); // Initially hide the text
    }

    public void ShowPopUpText(string text)
    {
        // Activate the pop-up text
        activePopUpText.gameObject.SetActive(true);

        // Set the text
        activePopUpText.text = text;

        // Animate the text (fade out and scale down)
        activePopUpText.DOFade(0f, popUpDuration).SetEase(Ease.InOutSine);
        activePopUpText.transform.DOScale(Vector3.zero, popUpDuration).SetEase(Ease.InOutSine);

        // Reset the text visibility and scale after the animation to reuse it
        DOVirtual.DelayedCall(popUpDuration, () =>
        {
            activePopUpText.gameObject.SetActive(false); // Hide the text again for reuse
            activePopUpText.transform.localScale = Vector3.one; // Reset the scale
            activePopUpText.color = new Color(activePopUpText.color.r, activePopUpText.color.g, activePopUpText.color.b, 1); // Reset alpha
        });
    }
}
