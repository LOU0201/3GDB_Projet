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
    public ResetTom resetTom; // Reference to the ResetTom script
    public Collectible collectible; // Reference to the Collectible script

    [Header("Animation Settings")]
    public float starDropDuration = 0.5f;
    public float starBounceStrength = 0.3f;

    private void Start()
    {
        // Ensure stars and texts are hidden initially
        foreach (var star in stars)
        {
            star.gameObject.SetActive(false);
        }
        foreach (var text in challengeTexts)
        {
            text.gameObject.SetActive(false);
        }
    }

    public void UpdateStarRating()
    {
        Debug.Log("Updating Star Rating...");

        // Activate and update challenge texts
        for (int i = 0; i < challengeTexts.Length; i++)
        {
            challengeTexts[i].gameObject.SetActive(true);
        }

        // Check challenge conditions
        bool[] challengeStatus = new bool[3];

        // Challenge 1: Reached Minimum Sortie (Level Complete)
        challengeStatus[0] = resetTom.playerScore >= resetTom.minsortie && !resetTom.Max;

        // Challenge 2: Reached Max Sortie (Bonus Objective)
        challengeStatus[1] = resetTom.playerScore >= resetTom.maxsortie;

        // Challenge 3: Collected Item & No Undo Used (if applicable)
        challengeStatus[2] = (collectible != null && collectible.collecté) ||
                            (!resetTom.annule || !resetTom._return);

        // Update UI based on challenge status
        for (int i = 0; i < challengeStatus.Length; i++)
        {
            challengeTexts[i].color = challengeStatus[i] ? Color.green : Color.red;

            if (challengeStatus[i])
            {
                ActivateStarWithAnimation(stars[i]);
            }
            else
            {
                stars[i].gameObject.SetActive(false); // Hide if failed
            }
        }
    }

    private void ActivateStarWithAnimation(Image star)
    {
        if (star == null) return;

        star.gameObject.SetActive(true);
        star.transform.localScale = Vector3.zero;

        // Bounce animation using DOTween
        star.transform.DOScale(1f, starDropDuration)
            .SetEase(Ease.OutBounce)
            .OnComplete(() => {
                // Optional: Add sparkle effect here
            });
    }
}
