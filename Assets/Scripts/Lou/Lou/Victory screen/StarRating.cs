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
        [HideInInspector] public bool completed; // Track completion state
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

    private Collectible[] allCollectibles;
    private bool initialized = false;

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        if (initialized) return;

        // Hide all stars initially
        foreach (var star in stars)
        {
            star.gameObject.SetActive(false);
        }

        // Initialize challenge texts
        foreach (var challenge in challenges)
        {
            if (challenge.text != null)
            {
                challenge.text.gameObject.SetActive(true);
                // Special handling for NoUndoUsed (green by default)
                if (challenge.type == ChallengeType.NoUndoUsed)
                {
                    challenge.text.color = Color.green;
                }
                else
                {
                    challenge.text.color = Color.red;
                }
            }
        }

        allCollectibles = FindObjectsOfType<Collectible>();
        initialized = true;
    }

    public void UpdateStarRating()
    {
        Initialize(); // Ensure initialization

        int starsEarned = 0;

        // Check each active challenge
        for (int i = 0; i < challenges.Length; i++)
        {
            if (!challenges[i].isActive) continue;

            challenges[i].completed = CheckChallengeCompletion(challenges[i].type);
            UpdateChallengeUI(challenges[i]);

            if (challenges[i].completed)
            {
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
                return resetTom.playerExitCount >= 1;

            case ChallengeType.MinExits:
                return resetTom.playerExitCount >= resetTom.minExitCount;

            case ChallengeType.MaxExits:
                return resetTom.playerExitCount >= resetTom.maxExitCount;

            case ChallengeType.CollectCollectible:
                /*foreach (var collectible in allCollectibles)
                {
                    if (collectible != null && collectible.collected == true)
                        return true;
                }
                return false;*/
                return resetTom.collectable != null && resetTom.collectable.collected;

            case ChallengeType.NoUndoUsed:
                return !resetTom.undoUsed;

            default:
                return false;
        }
    }

    private void UpdateChallengeUI(Challenge challenge)
    {
        if (challenge.text == null) return;

        challenge.text.color = challenge.completed ? Color.green : Color.red;
    }

    private void AnimateStar(Image star)
    {
        if (star == null) return;

        star.gameObject.SetActive(true);
        star.transform.localScale = Vector3.zero;

        Sequence seq = DOTween.Sequence();
        seq.Append(star.transform.DOScale(1.2f, starAnimationDuration).SetEase(Ease.OutBack));
        seq.Append(star.transform.DOScale(1f, starAnimationDuration * 0.5f));
    }
}