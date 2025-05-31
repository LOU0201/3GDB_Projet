using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class Barrière : MonoBehaviour
{
    public float detectionRadius = 2f;
    public LayerMask playerLayer;
    private bool isPlayerNear = false;
    private bool hasEnoughStars = false;
    private bool hasBeenDestroyed = false;
    private Sequence flashSequence;
    public GameObject PancementMur1;
    public GameObject PancementMur2;
    public GameObject PancementMur3;



    [Header("References")]
    public Boite SB;
    public GameObject CR;
    public int StarsNeeded;
    public GameManager GameManagerInstance;
    public TMP_Text StarCountText;
    public Image StarImage;
    public GameObject DestructionParticlesGO; // Reference to the GameObject containing the Particle System

    // Start is called before the first frame update
    void Start()
    {
        // Ensure GameManagerInstance is assigned
        if (GameManagerInstance == null)
        {
            GameManagerInstance = GameManager.Instance;
            if (GameManagerInstance == null)
            {
                Debug.LogError("GameManager not found!");
                enabled = false;
                return;
            }
        }

        // Ensure UI elements are assigned
        if (StarCountText == null || StarImage == null)
        {
            Debug.LogError("StarCountText or StarImage not assigned!");
            enabled = false;
            return;
        }

        UpdateStarCountText();
        CheckInitialPlayerProximity();

        // Ensure the particle GameObject is initially inactive (optional, if you manage its state elsewhere)
        if (DestructionParticlesGO != null && DestructionParticlesGO.activeSelf)
        {
            DestructionParticlesGO.SetActive(false);
        }
    }

    void CheckInitialPlayerProximity()
    {
        Collider[] playerColliders = Physics.OverlapSphere(transform.position, detectionRadius, playerLayer);
        if (playerColliders.Length > 0)
        {
            isPlayerNear = true;
            CheckAndHandleBarrier();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasBeenDestroyed)
        {
            // Check for player proximity every frame
            Collider[] playerColliders = Physics.OverlapSphere(transform.position, detectionRadius, playerLayer);
            isPlayerNear = playerColliders.Length > 0;

            hasEnoughStars = (GameManagerInstance != null && GameManagerInstance.étoiles >= StarsNeeded);

            UpdateStarCountText();
            CheckAndHandleBarrier();
        }
    }

    void CheckAndHandleBarrier()
    {
        if (isPlayerNear && GameManagerInstance != null && !hasBeenDestroyed)
        {
            if (hasEnoughStars)
            {
                // Enough stars, turn text green and potentially flash
                StarCountText.color = Color.green;
                StartFlashing(); // Keep flashing logic even with enough stars

                // Set the particle GameObject to active
                if (DestructionParticlesGO != null)
                {
                    DestructionParticlesGO.SetActive(true);
                }

                // Destroy immediately
                DestroyBarrier();
            }
            else
            {
                // Not enough stars, flash the text
                StartFlashing();
            }
        }
        else
        {
            StopFlashing();
            if (StarCountText != null && GameManagerInstance != null && !hasEnoughStars)
            {
                StarCountText.color = Color.white; // Reset color when player leaves
            }
        }
    }

    void UpdateStarCountText()
    {
        if (StarCountText != null && GameManagerInstance != null)
        {
            StarCountText.text = $"{GameManagerInstance.étoiles}/{StarsNeeded}";
            if (!hasEnoughStars && !isPlayerNear)
            {
                StarCountText.color = Color.white; // Default color when player is not near and not enough stars
            }
            else if (hasEnoughStars)
            {
                StarCountText.color = Color.green; // Text is green when enough stars
            }
        }
    }

    void StartFlashing()
    {
        if (StarCountText != null && flashSequence == null)
        {
            flashSequence = DOTween.Sequence();
            flashSequence.Append(StarCountText.DOColor(Color.red, 0.5f));
            flashSequence.Append(StarCountText.DOColor(StarCountText.color, 0.5f)); // Flash between red and current color
            flashSequence.SetLoops(-1, LoopType.Yoyo);
        }
    }

    void StopFlashing()
    {
        if (flashSequence != null)
        {
            flashSequence.Kill();
            flashSequence = null;
        }
    }

    void DestroyBarrier()
    {
        if (!hasBeenDestroyed)
        {

            Destroy(PancementMur1);
            Destroy(PancementMur2);
            Destroy(PancementMur3);

            Destroy(gameObject);
            Destroy(StarImage.gameObject);
            Destroy(StarCountText.gameObject);
            FMODUnity.RuntimeManager.PlayOneShot("event:/V3/System/MonolithExplode");
            hasBeenDestroyed = true;
            Destroy(this);
            print("Destroy Barrière fait");
        }
        else
        {
            print("erreure");
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}