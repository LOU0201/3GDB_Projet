using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class StarRating : MonoBehaviour
{
    [Header("Stars & Text")]
    public Image[] stars; // 3 UI stars (Set inactive by default)
    public TMP_Text[] challengeTexts; // 3 challenge descriptions (TMP)

    [Header("Challenge Conditions")]
    public bool levelCompleted = false;
    public bool collectedItem = false;
    public bool noUndoUsed = false;

    [Header("Animation Settings")]
    public float dropDuration = 0.5f;

    void Start()
    {
        // Hide all stars initially
        foreach (var star in stars)
        {
            star.gameObject.SetActive(false);
        }

        CheckChallenges();
    }

    void CheckChallenges()
    {
        bool[] challengeStatus = { levelCompleted, collectedItem, noUndoUsed };

        for (int i = 0; i < challengeStatus.Length; i++)
        {
            if (challengeStatus[i])
            {
                challengeTexts[i].color = Color.green; // Set text to green (Challenge passed)
                StartCoroutine(ShowStarAnimation(stars[i])); // Play animation
            }
            else
            {
                challengeTexts[i].color = Color.red; // Set text to red (Challenge failed)
            }
        }
    }

    IEnumerator ShowStarAnimation(Image star)
    {
        star.gameObject.SetActive(true); // Activate star
        star.transform.localScale = Vector3.zero; // Start scale at 0 (invisible)

        // Tweening Animation (Scale from 0 to 1 with bounce effect)
        star.transform.DOScale(1, dropDuration).SetEase(Ease.OutBounce);

        yield return new WaitForSeconds(0.2f); // Small delay before next star animates
    }
}
