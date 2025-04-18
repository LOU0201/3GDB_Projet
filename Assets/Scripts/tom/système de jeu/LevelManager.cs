using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class LevelManager : MonoBehaviour
{
    [Header("Level Data")]
    public LevelData levelData;

    [Header("Challenge texts")]
    public TMP_Text scoreText;
    public TMP_Text scoreText3;
    public TMP_Text scoreText4;

    [Header("Min Max Sorties")]
    public int playerExitCount = 0;
    public int minExitCount;
    public int maxExitCount;
    public bool max = false;
    public GameObject screen;

    [Header("Collectible")]
    public Collectible collectable;
    public bool undoForbidden = false;
    public bool undoUsed = false;
    public GameObject spaceButton;
    public TMP_Text nextLevel;
    public ListeTom LT;

    private StarRating starRatingSystem;

    void Start()
    {
        if (scoreText != null)
        {
            scoreText.text = "Sorties: " + playerExitCount.ToString() + "/" + maxExitCount.ToString();
            scoreText3.text = "0/1";
            scoreText4.text = "Undo: Not Used";
        }

    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && playerExitCount >= minExitCount && playerExitCount < maxExitCount)
        {
            screen.SetActive(true);

        }
        if (collectable != null && collectable.collected)
        {
            scoreText3.text = "1/1";
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            undoUsed = true;
            scoreText4.text = "Undo: Used";
        }
    }

    public void Rappatriment(Transform joueur)
    {
        LT.RefrecheIndex();
        playerExitCount += 1;
        joueur.transform.position = this.transform.position + new Vector3(0, 1, 0);
        if (scoreText != null)
        {
            scoreText.text = "Sorties: " + playerExitCount.ToString() + "/" + maxExitCount.ToString();
        }


        bool levelEnded = false;
        if (playerExitCount == minExitCount)
        {
            if (!max)
            {
                spaceButton.SetActive(true);
                levelEnded = true;
                nextLevel.gameObject.SetActive(true);
            }
            else
            {
                Debug.Log("INPUT DE FIN");
            }
        }
        if (playerExitCount == maxExitCount)
        {
            screen.SetActive(true);
            levelEnded = true;
        }

        if (playerExitCount >= maxExitCount)
        {
            screen.SetActive(true);
            levelEnded = true;
        }

        if (levelEnded)
        {
            CheckObjectives();
        }
    }

    public void CheckObjectives()
    {
        for (int i = 0; i < levelData.objectiveTypes.Length; i++)
        {
            ObjectiveType objectiveType = levelData.objectiveTypes[i];
            switch (objectiveType)
            {
                case ObjectiveType.CompleteLevel:
                    levelData.objectivesCompleted[i] = true;
                    break;
                case ObjectiveType.CollectCollectable:
                    if (collectable.collected == true)
                    {
                        levelData.objectivesCompleted[i] = true;
                    }
                    break;
                case ObjectiveType.NoUndo:
                    if (undoUsed == false)
                    {
                        levelData.objectivesCompleted[i] = true;
                    }
                    break;
                case ObjectiveType.MinExits:
                    levelData.objectivesCompleted[i] = true;
                    break;
                case ObjectiveType.MaxExits:
                    levelData.objectivesCompleted[i] = true;
                    break;
            }
        }

    }
    public void HandleCollectibleCollected()
    {
        // Update UI
        if (scoreText3 != null) scoreText3.text = "1/1";

        // Update star rating
        if (starRatingSystem != null) starRatingSystem.UpdateStarRating();

        // Clean up collectible later
        StartCoroutine(DelayedCollectibleCleanup());
    }

    IEnumerator DelayedCollectibleCleanup()
    {
        yield return new WaitForSeconds(1f); // Wait until rating is updated
        if (collectable != null)
        {
            Destroy(collectable.gameObject);
        }
    }
}
