using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Collectible : MonoBehaviour
{
    [Header("References")]
    public Transform joueur;
    public RectTransform collectibleUIElement;
    public GameObject objectToActivateOnComplete;

    [Header("Settings")]
    public float flySpeed = 1f; // Modifiable fly speed
    public float flyUpDistance = 100f;
    public float initialScale = 0.5f;
    public float finalScale = 0.2f;
    public float collectionRadius = 0.1f; // Small radius for precise collection

    public bool collected;
    private bool isAnimating;

    void Update()
    {
        if (!collected && !isAnimating && Vector3.Distance(joueur.position, transform.position) <= collectionRadius)
        {
            collected = true;
            isAnimating = true;
            StartCoroutine(CollectAnimation());
        }
    }

    IEnumerator CollectAnimation()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Placeholders/Items/itemcollect");
        GetComponent<Collider>().enabled = false;

        // Create the flying object
        GameObject flyingObj = new GameObject("FlyingCollectible");
        flyingObj.transform.SetParent(collectibleUIElement.parent, false);
        Image flyingImage = flyingObj.AddComponent<Image>();
        flyingImage.sprite = GetComponent<SpriteRenderer>().sprite;

        RectTransform flyingRT = flyingObj.GetComponent<RectTransform>();
        flyingRT.position = RectTransformUtility.WorldToScreenPoint(Camera.main, transform.position);
        flyingRT.localScale = Vector3.one * initialScale;

        // Calculate durations based on flySpeed
        float flyUpDuration = 0.3f / flySpeed;
        float flyToUIDuration = 0.7f / flySpeed;

        Sequence seq = DOTween.Sequence();
        seq.Append(flyingRT.DOMoveY(flyingRT.position.y + flyUpDistance, flyUpDuration).SetEase(Ease.OutQuad));
        seq.Append(flyingRT.DOMove(collectibleUIElement.position, flyToUIDuration).SetEase(Ease.InQuad));
        seq.Join(flyingRT.DOScale(finalScale, flyToUIDuration));
        seq.OnComplete(() => {
            if (objectToActivateOnComplete != null)
            {
                objectToActivateOnComplete.SetActive(true);
            }
            Destroy(flyingObj);
            gameObject.SetActive(false);
        });

        yield return seq.WaitForCompletion();
    }
}