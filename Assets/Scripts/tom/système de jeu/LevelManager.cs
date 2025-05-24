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
    public static int playerExitCount = 0;
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

    [Header("Son Bulle")]
    public MoveDownWard MDW;
    
    private StarRating starRatingSystem;

    void Start()
    {
        playerExitCount = 0;
        LoadLevelProgress();
        if (scoreText != null)
        {
            scoreText.text = "Sorties: " + playerExitCount.ToString() + "/" + maxExitCount.ToString();
            scoreText3.text = "0/1";
            scoreText4.text = "Undo: Not Used";
        }

    }
    public void Update()
    {
        if (scoreText != null)
        {
            scoreText.text = "Sorties: " + playerExitCount.ToString() + "/" + maxExitCount.ToString();
        }
        if (Input.GetKeyDown(KeyCode.Space) && playerExitCount >= minExitCount && playerExitCount < maxExitCount && minExitCount != -1)
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
        if (Input.GetKeyDown(KeyCode.F1)) // Press F1 to clear all saved data
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
            Debug.Log("All PlayerPrefs deleted.");
        }
    }

    public void Rappatriment(Transform joueur)
    {
        LT.RefrecheIndex();
        if (MDW)
        {
            MDW.son = true;
        }
        playerExitCount += 1;
        joueur.transform.position = this.transform.position + new Vector3(0, 1, 0);

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
            if (screen)
            {
                screen.SetActive(true);
            }
            levelEnded = true;
        }

        if (playerExitCount >= maxExitCount)
        {
            if (screen)
            {
                screen.SetActive(true);
            }
            levelEnded = true;
        }

        if (levelEnded)
        {
            CheckObjectives();
            SaveLevelProgress(); //Save the updated level data to persistence
            GameManager.Instance.starsUp();
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
                    levelData.objectivesCompleted[i] = !undoUsed;
                    break;
                case ObjectiveType.MinExits:
                    levelData.objectivesCompleted[i] = (playerExitCount == minExitCount);
                    break;
                case ObjectiveType.MaxExits:
                    levelData.objectivesCompleted[i] = (playerExitCount == maxExitCount);
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

        // Clean up collectible
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

    private string GetPlayerPrefsKey(int objectiveIndex)
    {
        // Creates a unique key for each objective of each LevelData instance.
        return levelData.name + "_Objective_" + objectiveIndex;
    }

    public void SaveLevelProgress()
    {
        if (levelData == null)
        {
            Debug.LogWarning("LevelData is not assigned in LevelManager. Cannot save progress.");
            return;
        }

        for (int i = 0; i < levelData.objectivesCompleted.Length; i++)
        {
            // PlayerPrefs.SetInt stores integers. We convert bool (true/false) to int (1/0).
            PlayerPrefs.SetInt(GetPlayerPrefsKey(i), levelData.objectivesCompleted[i] ? 1 : 0);
        }
        PlayerPrefs.Save(); // Ensures data is written to disk immediately.
        Debug.Log($"Level progress saved for {levelData.name}.");
    }

    public void LoadLevelProgress()
    {
        if (levelData == null)
        {
            Debug.LogWarning("LevelData is not assigned in LevelManager. Cannot load progress.");
            return;
        }

        for (int i = 0; i < levelData.objectivesCompleted.Length; i++)
        {
            // PlayerPrefs.GetInt retrieves an integer. The second argument (0) is the default value
            // if the key doesn't exist yet (meaning the objective hasn't been completed).
            levelData.objectivesCompleted[i] = PlayerPrefs.GetInt(GetPlayerPrefsKey(i), 0) == 1;
        }
        Debug.Log($"Level progress loaded for {levelData.name}.");
    }
}
