using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Fadeout : MonoBehaviour
{
    public class FadeElement
    {
        public CanvasGroup canvasGroup; // For fading CanvasGroups
        public TextMeshProUGUI textElement; // For fading TMP texts
        public GameObject gameObject; // For disabling GameObjects
    }

    public List<FadeElement> fadeElements = new List<FadeElement>(); // List of elements to fade
    public float fadeDuration = 2f; // Duration for each fade-out
    public float fadeDelay = 1f; // Delay between fading each element

    public CameraTurn cameraRotateScript; // Reference to the camera rotation script

    private void Start()
    {
        StartCoroutine(FadeOutList());
    }

    private IEnumerator FadeOutList()
    {
        foreach (FadeElement element in fadeElements)
        {
            if (element.canvasGroup != null)
            {
                yield return StartCoroutine(FadeOutCanvasGroup(element.canvasGroup));
                element.canvasGroup.gameObject.SetActive(false);
            }
            else if (element.textElement != null)
            {
                yield return StartCoroutine(FadeOutTMPText(element.textElement));
                element.textElement.gameObject.SetActive(false);
            }
            else if (element.gameObject != null)
            {
                yield return StartCoroutine(FadeOutGameObject(element.gameObject));
                element.gameObject.SetActive(false);
            }

            // Wait before fading the next element
            yield return new WaitForSeconds(fadeDelay);
        }

        // Start the camera rotation after all fade-outs
        if (cameraRotateScript != null)
        {
            //cameraRotateScript.StartRotation();
        }
    }

    private IEnumerator FadeOutCanvasGroup(CanvasGroup canvasGroup)
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = 0f;
    }

    private IEnumerator FadeOutTMPText(TextMeshProUGUI textElement)
    {
        float elapsedTime = 0f;
        Color originalColor = textElement.color;

        while (elapsedTime < fadeDuration)
        {
            textElement.color = new Color(
                originalColor.r,
                originalColor.g,
                originalColor.b,
                Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration)
            );
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        textElement.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
    }

    private IEnumerator FadeOutGameObject(GameObject gameObject)
    {
        // Optionally add custom fade logic here (e.g., fading materials).
        yield return new WaitForSeconds(fadeDuration);
    }
    
}
