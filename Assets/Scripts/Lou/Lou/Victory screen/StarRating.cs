using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class StarRating : MonoBehaviour
{
    [System.Serializable]
    public struct Challenge
    {
        public bool isActive;
        public TMP_Text text;
        public ChallengeType type;
    }

    public enum ChallengeType
    {
        FinishLevel,
        MinExits,
        MaxExits,
        CollectCollectible,
        NoUndoUsed
    }

    [Header("Stars")]
    public Image[] stars;

    [Header("Challenges")]
    public Challenge[] challenges;

    [Header("References")]
    public LevelManager resetTom;

    [Header("Animation Settings")]
    public float starAnimationDuration = 0.5f;
    public Ease starAnimationEase = Ease.OutBounce;

    private Collectible[] allCollectibles; // Array to hold all collectible scripts

    private void Start()
    {
        // Hide all stars initially
        foreach (var star in stars)
        {
            star.gameObject.SetActive(false);
        }

        // Hide all challenge texts initially
        foreach (var challenge in challenges)
        {
            if (challenge.text != null)
                challenge.text.gameObject.SetActive(false);
        }

        // Find all Collectible scripts in the scene at the start
        allCollectibles = FindObjectsOfType<Collectible>();
    }

    public void UpdateStarRating()
    {
        int starsEarned = 0;

        // Check each active challenge
        for (int i = 0; i < challenges.Length; i++)
        {
            if (!challenges[i].isActive) continue;

            bool challengeCompleted = CheckChallengeCompletion(challenges[i].type);
            UpdateChallengeUI(challenges[i], challengeCompleted);

            if (challengeCompleted)
            {
                // Only animate stars for the first 3 completed challenges
                if (starsEarned < stars.Length)
                {
                    AnimateStar(stars[starsEarned]);
                    starsEarned++;
                }
            }
        }
    }

    private bool CheckChallengeCompletion(ChallengeType type)
    {
        switch (type)
        {
            case ChallengeType.FinishLevel:
                return resetTom.playerExitCount >= resetTom.minExitCount;

            case ChallengeType.MinExits:
                return resetTom.playerExitCount >= resetTom.minExitCount;

            case ChallengeType.MaxExits:
                return resetTom.playerExitCount >= resetTom.maxExitCount;

            case ChallengeType.CollectCollectible:
                // Check if at least one collectible has been collected
                foreach (var collectible in allCollectibles)
                {
                    if (collectible.collected)
                    {
                        return true;
                    }
                }
                return false;

            case ChallengeType.NoUndoUsed:
                return !resetTom.undoUsed; // True if undo was NOT used

            default:
                return false;
        }
    }

    private void UpdateChallengeUI(Challenge challenge, bool completed)
    {
        if (challenge.text == null) return;

        // Activate the text (hidden by default)
        challenge.text.gameObject.SetActive(true);

        // Special case for NoUndoUsed (inverted logic)
        if (challenge.type == ChallengeType.NoUndoUsed)
        {
            challenge.text.color = completed ? Color.green : Color.red;
        }
        else
        {
            challenge.text.color = completed ? Color.green : Color.red;
        }
    }

    private void AnimateStar(Image star)
    {
        if (star == null) return;

        star.gameObject.SetActive(true);
        star.transform.localScale = Vector3.zero;

        // Bounce animation
        star.transform.DOScale(1f, starAnimationDuration)
            .SetEase(starAnimationEase);
    }
}
