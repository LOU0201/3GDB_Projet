using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Collectible : MonoBehaviour
{
    public Transform joueur;
    public RectTransform collectibleUIElement;
    public Camera mainCamera;
    public GameObject objectToActivateOnComplete;

    private int x;
    private int z;
    private int y;
    private float tempx;
    private float tempz;
    private float tempy;
    public int max;
    public bool collecté;

    void Start()
    {
        z = Random.Range(2, 16);
        tempz = z;
        tempz += 0.5f;
        x = Random.Range(1, 16);
        tempx = x;
        tempx += 0.5f;
        y = Random.Range(1, 7);
        tempy = y;
        tempy += 0.5f;

        if (mainCamera == null)
            mainCamera = Camera.main;
    }

    void Update()
    {
        Vector3 coordonnees = transform.position;
        Vector3 CJ = joueur.position;

        if (CJ == coordonnees && !collecté)
        {
            collecté = true;
            StartCoroutine(CollectAnimation());
        }
    }

    IEnumerator CollectAnimation()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Placeholders/Items/itemcollect");
        GetComponent<Collider>().enabled = false;

        Vector3 screenPos = mainCamera.WorldToScreenPoint(transform.position);

        GameObject flyingObj = new GameObject("FlyingCollectible");
        flyingObj.transform.SetParent(collectibleUIElement.parent, false);
        Image flyingImage = flyingObj.AddComponent<Image>();
        flyingImage.sprite = GetComponent<SpriteRenderer>().sprite;

        flyingObj.GetComponent<RectTransform>().position = screenPos;
        flyingObj.GetComponent<RectTransform>().localScale = Vector3.one * 0.5f;

        Sequence seq = DOTween.Sequence();

        seq.Append(flyingObj.transform.DOMoveY(screenPos.y + 100f, 0.3f).SetEase(Ease.OutQuad));
        seq.Append(flyingObj.transform.DOMove(collectibleUIElement.position, 0.7f).SetEase(Ease.InQuad));
        seq.Join(flyingObj.transform.DOScale(0.2f, 0.7f));

        yield return seq.WaitForCompletion();

        // Activate the GameObject if it's assigned
        if (objectToActivateOnComplete != null)
        {
            objectToActivateOnComplete.SetActive(true);
        }

        Destroy(flyingObj);
        Destroy(gameObject);
    }
}
