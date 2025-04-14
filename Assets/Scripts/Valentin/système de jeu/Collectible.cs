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
    private LevelManager levelManager;

    [Header("Collection Settings")]
    public float collectionRadius = 0.5f;
    public bool collected;

    [Header("Animation Settings")]

    public float flySpeed = 1f;
    public float flyUpDistance = 100f;
    public float initialScale = 0.5f;
    public float finalScale = 0.2f;
    public float rotationSpeed = 180f;

    public AnimationCurve flyUpCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    public AnimationCurve flyToUICurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    public AnimationCurve scaleCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    private bool isAnimating;
    private StarRating starRating;

    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }
    void Update()
    {
        if (!collected && !isAnimating && Vector3.Distance(joueur.position, transform.position) <= collectionRadius)
        {
            collected = true; // Flag as collected immediately
            isAnimating = true;
            StartCoroutine(CollectAnimation());
        }
    }

    IEnumerator CollectAnimation()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Placeholders/Items/itemcollect");
        //GetComponent<Collider>().enabled = false;

        // Create flying object
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

        // Upward movement
        seq.Append(flyingRT.DOMoveY(flyingRT.position.y + flyUpDistance, flyUpDuration)
           .SetEase(flyUpCurve));

        // Rotation during flight
        seq.Join(flyingRT.DORotate(new Vector3(0, 0, rotationSpeed * flyUpDuration), flyUpDuration)
           .SetRelative());

        // Movement to UI element
        seq.Append(flyingRT.DOMove(collectibleUIElement.position, flyToUIDuration))
           .SetEase(flyToUICurve);

        // Scaling animation
        seq.Join(flyingRT.DOScale(finalScale, flyToUIDuration))
           .SetEase(scaleCurve);

        // Continue rotation
        seq.Join(flyingRT.DORotate(new Vector3(0, 0, rotationSpeed * flyToUIDuration), flyToUIDuration)
           .SetRelative());

        seq.OnComplete(() => {
            if (objectToActivateOnComplete != null)
            {
                objectToActivateOnComplete.SetActive(true);
            }
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider>().enabled = false;
            levelManager.HandleCollectibleCollected();
            Destroy(flyingObj);
            //gameObject.SetActive(false);
        });

        yield return seq.WaitForCompletion();
    }

}